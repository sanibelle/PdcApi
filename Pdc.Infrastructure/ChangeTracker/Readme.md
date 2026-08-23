# ChangeTracker — Versioned Change Application

This folder implements the logic that applies changes (Add / Update / Delete) to
`Changeable` domain entities (e.g. `RealisationContext`, `MinisterialCompetencyElement`,
`PerformanceCriteria`, course framework changeables), while optionally recording
those changes against a specific **version** (`ChangeRecord`).

There are two independent axes to understand:

1. **`IChangeApplier<T, TParent, TEntity>`** — *how* a change is applied to the database
   (plain save vs. version-history-aware save).
2. **`IChangeTracker`** — *whether* the change is logged as a `ChangeDetailEntity` for
   history/audit purposes, or simply ignored.

They are combined at the repository level to produce four possible behaviors.

---

## 1. `IChangeApplier<T, TParent, TEntity>`

### `AUntrackedChangeApplier<T, TParent, TEntity>` (base class)
- Applies changes **directly** to `AppDbContext` (map → add/update/remove → `SaveChangesAsync`).
- Delegates the audit logging decision entirely to the `IChangeTracker` passed in.
- Has no notion of "was this entity already changed within the current version?".

### `ATrackedChangeApplier<T, TParent, TEntity>` (inherits `AUntrackedChangeApplier`)
- Adds **version-collapsing logic** on top of the base behavior:
  - **Update**: checks if a `ChangeDetailEntity` already exists for this entity *within the
    current `ChangeRecord`*. If none exists, falls back to the untracked behavior (first
    change of the version). If one exists, it mutates the entity in place based on the
    existing `ChangeType` (`Add`/`Update`), so editing the same field twice in one version
    doesn't create duplicate history rows.
  - **Delete**: reconciles deletions against changes already recorded in the same version
    (e.g. an entity added *and* deleted in the same version is fully removed instead of
    leaving an "add" + "delete" history trail).

```mermaid
sequenceDiagram
    participant Repo as CompetencyRepository
    participant ChangeRecordRepo as ChangeRecordRepository
    participant Applier as UntrackedRealisationContextChangeApplier
    participant Ctx as AppDbContext
    participant Tracker as NoOpChangeDetailsTracker

    Repo->>Tracker: new NoOpChangeDetailsTracker()
    Repo->>ChangeRecordRepo: AddChangeRecord(competency.ChangeRecord)
    ChangeRecordRepo-->>Repo: changeRecord

    Repo->>Ctx: Add CompetencyEntity
    Ctx-->>Repo: addedCompetencyEntity

    loop for each RealisationContext rc
        Repo->>Applier: Add(changeRecord, competencyEntity, rc, tracker)
        Applier->>Applier: Map T to TEntity
        Applier->>Applier: AssignParent(entity, parent)
        Applier->>Ctx: AddAsync(entity)
        Applier->>Ctx: SaveChangesAsync()
        Applier->>Tracker: TrackAdd(entity, changeRecord.Id)
        Tracker-->>Applier: no-op (does nothing)
        Applier-->>Repo: added.Entity
    end

    Repo->>Ctx: SaveChangesAsync()
    Repo->>Ctx: CommitAsync()
    Note over Repo,Ctx: No ChangeDetailEntity rows created
```

```mermaid
sequenceDiagram
    participant Repo as CompetencyRepository
    participant Applier as TrackedRealisationContextChangeApplier
    participant Base as AUntrackedChangeApplier (base)
    participant Ctx as AppDbContext
    participant Tracker as ChangeDetailsTracker

    Repo->>Tracker: new ChangeDetailsTracker(context)
    Repo->>Applier: Update(changeRecord, toUpdate, tracker)

    Applier->>Ctx: Find TEntity by Id
    Ctx-->>Applier: entityToUpdate

    Applier->>Ctx: Find ChangeDetailEntity for (changeRecord.Id, entity.Id)
    Ctx-->>Applier: changeDetail (may be null)

    alt First edit in this version (changeDetail == null)
        Applier->>Base: base.Update(changeRecord, toUpdate, tracker)
        Base->>Ctx: Map + Update entity
        Base->>Ctx: SaveChangesAsync()
        Base->>Tracker: TrackUpdate(entity, oldValue, changeRecord.Id)
        Tracker->>Ctx: Insert new ChangeDetailEntity (ChangeType=Update, OldValue)
        Base-->>Applier: updated.Entity
    else Already changed in this version (changeDetail != null)
        Applier->>Applier: switch(changeDetail.ChangeType)
        Applier->>Applier: entity.Value = toUpdate.Value
        Note over Applier: Existing ChangeDetailEntity.OldValue is preserved,<br/>no new history row created
    end

    Applier-->>Repo: updated entity
    Repo->>Ctx: CommitAsync()
    Note over Repo,Ctx: History reflects "X → Z" instead of "X → Y → Z"
```