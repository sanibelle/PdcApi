import { test as base, Page } from '@playwright/test';

type TestFixtures = {
  authenticatedPage: Page;
  adminPage: Page;
  userPage: Page;
};

export const test = base.extend<TestFixtures>({
  authenticatedPage: async ({ page }, use) => {
    // Set default admin user
    await page.setExtraHTTPHeaders({
      'Test-User': 'admin@test.com'
    });
    await use(page);
  },

  adminPage: async ({ page }, use) => {
    await page.setExtraHTTPHeaders({
      'Test-User': 'admin@test.com'
    });
    await use(page);
  },

  userPage: async ({ page }, use) => {
    await page.setExtraHTTPHeaders({
      'Test-User': 'user@test.com'
    });
    await use(page);
  }
});

export { expect } from '@playwright/test';