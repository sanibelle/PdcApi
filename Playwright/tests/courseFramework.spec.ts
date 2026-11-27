import { Page } from '@playwright/test';
import { test, expect } from './test-fixtures';
import { selectWithDataTestId } from './testUtils';
test.describe("course framework", () => {
  test("Creating a full valid course framework", async ({
    adminPage,
  }) => {
    await createAndTestCourseFramework(adminPage, "123-A1B-AB");
  });
});

const createAndTestCourseFramework = async (adminPage: Page, code: string) => {
  await adminPage.goto("/plansCadres");
  await adminPage.waitForLoadState("networkidle");

  await adminPage
    .getByRole("row", { name: "Seededprogram Test Program of" })
    .getByRole("link")
    .click();
  await adminPage.getByTestId("create-course-framework-btn").first().click();

  // expect the modal to be visible
  await expect(adminPage.locator(".modal-overlay")).toBeVisible();

  await adminPage.locator('input[name="code"]').fill(code);
  await adminPage.locator('input[name="name"]').fill("Plan cadre test");
  await adminPage.locator('input[name="semester"]').fill("1");
  await adminPage.locator('input[name="theoryHours"]').fill("1");
  await adminPage.locator('input[name="laboratoryHours"]').fill("2");
  await adminPage.locator('input[name="personalWorkHours"]').fill("3");
  await selectWithDataTestId(adminPage, 'competencies');

  const codeInput = adminPage.locator('input[name="code"]');
  for (const invalidCode of ["12A-ABC-DE", "123-AB--DE", "123-ABC-D1", "123-ABC-DEF"]) {
    await codeInput.fill(invalidCode);
    await adminPage.getByTestId("submit-course-framework").first().click();
    await expect(codeInput).toHaveClass(/error/);
  }
  await codeInput.fill(code);
  await expect(codeInput).not.toHaveClass(/error/);

  const [response] = await Promise.all([
    adminPage.waitForResponse(
      (response) =>
        response.url().includes("/courseFramework") &&
        response.request().method() === "POST"
    ),
    adminPage.getByTestId("submit-course-framework").first().click(),
  ]);

  await expect(response.status()).toBe(201);
  await expect(adminPage.locator(".modal-overlay")).toBeHidden();
  await expect(adminPage.getByRole('cell', { name: code })).toBeVisible();
}
