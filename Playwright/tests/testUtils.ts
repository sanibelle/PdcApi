import type { Page } from '@playwright/test';

export async function selectWithDataTestId(page: Page, dataTestId: string, ...choiceIndex: number[]) {
  const selector = `[data-test-id="${dataTestId}"]`;
  await page.locator(selector).click();
  await page.locator(`${selector} .multiselect__content .multiselect__option`).nth(choiceIndex[0] ?? 0).click();
}
