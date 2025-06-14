import { test as base, Page } from '@playwright/test';

type TestFixtures = {
  authenticatedPage: Page;
  adminPage: Page;
  userPage: Page;
};

export const test = base.extend<TestFixtures>({
  userPage: async ({ page }, use) => {
    await page.setExtraHTTPHeaders({
      'Test-User': 'TestUser'
    });
    await use(page);
  },

  adminPage: async ({ page }, use) => {
    await page.setExtraHTTPHeaders({
      'Test-User': 'TestAdmin'
    });
    await use(page);
  },
});

export { expect } from '@playwright/test';