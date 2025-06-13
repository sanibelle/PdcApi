import { test, expect } from './test-fixtures';

test('Creating a full valid program of study', async ({ page }) => {
  await page.goto('/gestion');
  await page.waitForLoadState('networkidle');

  await page.locator('#create-program-btn').first().click();
  
  // expect the modal to be visible
  await expect(page.locator('.modal-overlay')).toBeVisible();
   
  await page.locator('input[name="name"]').fill('Creating a full valid program of study')
  await page.locator('input[name="code"]').fill('code1')
  await page.locator('select[name="programType"]').selectOption('1');
  await page.locator('input[name="monthsDuration"]').fill('24')
  await page.locator('input[name="specificDurationHours"]').fill('123')
  await page.locator('input[name="totalDurationHours"]').fill('123')
  await page.locator('[data-test-id="dp-input"]').fill("06/13/2025");
  // units
  await page.locator('input[name="specificUnits.wholeUnit"]').fill('1')
  await page.locator('select[name="specificUnits.numerator"]').selectOption('1');
  await page.locator('select[name="specificUnits.denominator"]').selectOption('2');

  await page.locator('input[name="optionalUnits.wholeUnit"]').fill('2')
  await page.locator('select[name="optionalUnits.numerator"]').selectOption('1');
  await page.locator('select[name="optionalUnits.denominator"]').selectOption('2');

  await page.locator('input[name="generalUnits.wholeUnit"]').fill('3')
  await page.locator('select[name="generalUnits.numerator"]').selectOption('2');
  await page.locator('select[name="generalUnits.denominator"]').selectOption('3');
  
  await page.locator('input[name="complementaryUnits.wholeUnit"]').fill('4')
  await page.locator('select[name="complementaryUnits.numerator"]').selectOption('1');
  await page.locator('select[name="complementaryUnits.denominator"]').selectOption('3');

  await page.locator('#submit-program').first().click();
  await expect(page.locator('.modal-overlay')).toBeHidden();

  // todo valider que le programme a été créé dans la liste
  // valider un 201 dans le network
});

test('Creating a minimal valid program of study', async ({ page }) => {
  await page.goto('/gestion');
  await page.waitForLoadState('networkidle');

  await page.locator('#create-program-btn').first().click();
  
  // expect the modal to be visible
  await expect(page.locator('.modal-overlay')).toBeVisible();
  
  await page.locator('input[name="name"]').fill('Creating a minimal valid program of study')
  await page.locator('input[name="code"]').fill('code2')
  await page.locator('select[name="programType"]').selectOption('2');
  await page.locator('input[name="monthsDuration"]').fill('24')
  await page.locator('input[name="specificDurationHours"]').fill('123')
  await page.locator('input[name="totalDurationHours"]').fill('123')
  await page.locator('[data-test-id="dp-input"]').fill("06/13/2025");
  // units
  await page.locator('input[name="specificUnits.wholeUnit"]').fill('1')
  await page.locator('input[name="optionalUnits.wholeUnit"]').fill('2')
});