import { test, expect } from "./test-fixtures";

test.describe("Roles", () => {
  test("Filtering an added role should not filter it", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/user");
    await adminPage.waitForLoadState("networkidle");

    const roleFilter = await adminPage.locator("input[name=rolesFilter]");
    const userFilter = await adminPage.locator("input[name=usersFilter]");
    const rolesContainer = await adminPage.getByTestId("roles-container");
    const numberOfRoles = await rolesContainer.locator(".item").count();
    const userRow = await adminPage.getByTestId("user-2");
    // user_ is the prefix for the generated users in the seeder.
    await expect(userRow).toContainText("user_");
    await userRow.click();

    // user should have no roles initially
    await roleFilter.fill("NonExistingRoleName");
    // checks if roleContaines has no children
    await expect(await rolesContainer.locator(".hidden").count()).toBe(
      numberOfRoles,
    );

    // adding the first role
    await roleFilter.fill("");
    await adminPage.getByTestId("role-0").click();

    // now the roles should have at least one element
    await roleFilter.fill("NonExistingRoleName");
    await expect(await rolesContainer.locator(".hidden").count()).toBe(
      numberOfRoles - 1,
    );

    await expect(userFilter).toBeDisabled()
  });
});
