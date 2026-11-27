import { test, expect } from './test-fixtures';
import { selectWithDataTestId } from './testUtils';

test('Creating a full valid program of study', async ({ adminPage }) => {
  await adminPage.goto('/administration/programmes');
  await adminPage.waitForLoadState('networkidle');

  await adminPage.locator('#create-program-btn').first().click();
  
  // expect the modal to be visible
  await expect(adminPage.locator('.modal-overlay')).toBeVisible();
   
  await adminPage.locator('input[name="name"]').fill('Creating a full valid program of study')
  await adminPage.locator('input[name="code"]').fill('123.A4')

  await selectWithDataTestId(adminPage, 'programType');

  await adminPage.locator('input[name="monthsDuration"]').fill('24')
  await adminPage.locator('input[name="specificDurationHours"]').fill('123')
  await adminPage.locator('input[name="totalDurationHours"]').fill('123')
  await adminPage.locator('[data-test-id="dp-input"]').fill("06/13/2025");
  // units
  await adminPage.locator('input[name="specificUnits.wholeUnit"]').fill('1')
  
    // units
  await selectWithDataTestId(adminPage, 'specificUnits.numerator');
  

  await adminPage.locator('input[name="optionalUnits.wholeUnit"]').fill('2')
  await selectWithDataTestId(adminPage, 'optionalUnits.denominator');

  await adminPage.locator('input[name="generalUnits.wholeUnit"]').fill('3')
  
  await adminPage.locator('input[name="complementaryUnits.wholeUnit"]').fill('4')
  await selectWithDataTestId(adminPage, 'complementaryUnits.numerator', 1);

  const [response] = await Promise.all([
    adminPage.waitForResponse(response => response.url().includes('/ProgramOfStudy') && response.request().method() === 'POST'),
    adminPage.locator('#submit-program').first().click()
  ]);

  expect(response.status()).toBe(201);
  await expect(adminPage.locator('.modal-overlay')).toBeHidden();
});

