// Créez d'abord un resolver personnalisé
using AutoMapper;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.Versioning;

public class ChangeRecordResolver : IValueResolver<object, object, ChangeRecordEntity>
{
    public ChangeRecordEntity Resolve(object source, object destination, ChangeRecordEntity destMember, ResolutionContext context)
    {
        ChangeRecord? version = null;
        // Détecter le type de l'objet source et extraire la Competency
        if (source is MinisterialCompetency competency)
        {
            version = FindChangeRecord(competency);
        }
        else if (source is ComplementaryInformation complementaryInformation)
        {
            version = FindChangeRecord(complementaryInformation);
        }
        else
        {
            throw new AutoMapperMappingException($"Type de source non pris en charge : {source.GetType()}");
        }
        var key = $"ChangeRecord_{version.Id}";
        context.TryGetItems(out Dictionary<string, object> entityCache);

        if (entityCache.TryGetValue(key, out var existingEntity))
            return (ChangeRecordEntity)existingEntity;

        var mapped = context.Mapper.Map<ChangeRecordEntity>(version);
        entityCache[key] = mapped;
        return mapped;
    }

    private ChangeRecord FindChangeRecord(MinisterialCompetency competency)
    {
        var version = competency.CurrentVersion;
        if (version == null)
            throw new AutoMapperMappingException($"{nameof(competency.CurrentVersion)} de {competency.GetType()} ne peut pas être null");
        return version;
    }

    private ChangeRecord FindChangeRecord(ComplementaryInformation complementaryInformation)
    {
        var version = complementaryInformation.WrittenOnVersion;
        if (version == null)
            throw new AutoMapperMappingException($"{nameof(complementaryInformation.WrittenOnVersion)} de {complementaryInformation.GetType()} ne peut pas être null");
        return version;
    }
}