<script setup lang="ts">
import { ref, reactive } from 'vue';
import { VueDraggable } from 'vue-draggable-plus';

const complete = (item: NestableItem, id: number | undefined = undefined): void => {
  if (!item.children) {
    item.isCompleted = item.isCompleted || id === undefined || item.id == id;
  } else {
    const seekedId = id === undefined || item.id == id ? undefined : id;
    item.children?.forEach((child) => complete(child, seekedId));
  }
};

const list = reactive<NestableItem[]>([
  {
    name: 'Analyser le projet de développement de l’application.',
    id: 5,
    children: [
      {
        name: 'Analyse juste des documents de conception.',
        id: 6,
        children: [
          { name: "Représentation d'éléments graphiques avancés.", id: 7 },
          {
            name: 'Compréhension des éléments graphiques avancés dans une charte graphique.',
            id: 8,
          },
        ],
      },
      {
        name: 'Détermination correcte des tâches à effectuer.',
        id: 9,
        children: [
          {
            name: 'Pertinence et exhaustivité des tâches à effectuer pour mettre en place la solution.',
            id: 10,
          },
          {
            name: 'Séquence des étapes de développement d’une application Web non transactionnelle.',
            id: 11,
          },
        ],
      },
    ],
  },
  {
    name: 'Préparer l’environnement de développement informatique.',
    id: 12,
    children: [
      {
        name: 'Installation correcte de la plateforme de développement Web et du système de gestion de base de données de développement.',
        id: 13,
        children: [
          {
            name: 'Procédures d’installation et configuration d’une suite de logiciels de développement (ex. : Visual Studio Code, Brackets).',
            id: 14,
          },
          {
            name: 'Installation et configuration d’un système de gestion de bases de données (ex. : MSSQL).',
            id: 15,
          },
        ],
      },
      {
        name: 'Installation correcte des logiciels et des bibliothèques.',
        id: 16,
        children: [
          {
            name: 'Procédures d’installation et configuration d’un mapping objet-relationnel (ORM) (ex. : Entity Framework).',
            id: 17,
          },
          {
            name: 'Utilisation d’un utilitaire d’importation de librairies/packages (ex. : Nuget).',
            id: 18,
          },
        ],
      },
    ],
  },
]);

const list2 = ref<NestableItem[]>([]);

const onMoveEnd = (id: number) => {
  for (const item of list) {
    complete(item, id);
  }
};
const onRemove = (id: number) => {
  const findAndRemove = (items: NestableItem[], id: number) => {
    const index = items.findIndex((item) => item.id === id);
    if (index !== -1) {
      items.splice(index, 1);
    }
    for (const item of items) {
      if (item.children && findAndRemove(item.children, id)) {
      }
    }
  };

  findAndRemove(list2.value, id);
};

const idsToHighLight = ref<number[]>([]);

const onHighlight = (ids: number[]) => {
  idsToHighLight.value = ids;
};
</script>

<template>
  <h1>Mes semaines</h1>
  <pre>
    {{ list }}
  </pre>
  <section id="building" class="py-20 bg-white flex flex-row items-center">
    <CommonNestedDraggable @end="onMoveEnd" v-model="list" :ids-to-highlight="idsToHighLight" />
    <VueDraggable v-model="list2" :animation="150" ghostClass="ghost" class="right min-h-[300px] min-w-[300px]"
      :group="{ name: 'syllabus', pull: false, put: true }">
      <template v-for="item in list2" :key="item.id" class="item">
        <SyllabusContent :item="item" @on-remove="onRemove" @onHighlight="onHighlight"></SyllabusContent>
      </template>
    </VueDraggable>
  </section>
</template>

<style>
.left {
  flex: 1;
}

.right {
  flex: 1;
}

.container {
  border: solid 1px black;
}

.item {
  background-color: red;
  line-height: 30px;
  text-align: center;
  cursor: move;
  border: solid 1px black;
}

.highlight {
  background-color: yellow;
}
</style>
