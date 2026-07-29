<template>
  <div class="grid-container">
    <LayoutTemplateHeader class="header" />
    <LayoutTemplateMainNav class="main-nav" />
    <div class="menu">
      <ul>
        <li>
          <NuxtLink to="/modules">Modules</NuxtLink>
        </li>
        <li>
          <NuxtLink to="/students">Students</NuxtLink>
        </li>
      </ul>
    </div>
    <div class="content">
      <slot />
    </div>
    <ModulesAdministrationComplementaryInformationTheContainer class="right-side-bar" />
    <footer class="footer bg-gray-900 text-white py-8">POC semaines plan de cours</footer>
  </div>
  <CommonTemplateAModal />
</template>

<style lang="scss" scoped>
  .grid-container {
    display: grid;
    grid-template-rows: 80px 1fr 90px;
    grid-template-columns: 320px 1fr auto;
    grid-column-gap: 0px;
    grid-row-gap: 0px;
    min-height: 100vh;
    grid-template-areas:
      'header main-nav main-nav'
      'menu content right-side-bar'
      'footer footer footer';
  }

  .header {
    grid-area: header;
  }

  .main-nav {
    grid-area: main-nav;
    display: flex;
    align-items: center;
    justify-content: space-around;
    background-color: #f8f8f8;
    padding: 0.5rem 0;
  }

  .wrapper {
    display: flex;
    align-items: flex-start;
    gap: 24px;
    padding: 24px;
    max-width: 1280px;
    margin: 0 auto;
  }

  .menu {
    grid-area: menu;
    background-color: #f8f8f8;
    padding: 1rem;
  }

  .content {
    justify-self: center;
    align-self: start;
  }

  .footer {
    grid-area: footer;
    text-align: center;
  }
</style>
