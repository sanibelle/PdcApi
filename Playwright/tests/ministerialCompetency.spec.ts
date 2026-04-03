import { test, expect } from "./test-fixtures";

test.describe("ministerial competency", () => {
  test("Creating a full valid ministerial competency", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programmes");
    await adminPage.waitForLoadState("networkidle");

    await adminPage
      .getByRole("row", { name: "Seededprogram Test Program of" })
      .getByRole("link")
      .click();
    await adminPage.getByTestId("create-competency-btn").first().click();

    // expect the modal to be visible
    await expect(adminPage.locator(".modal-overlay")).toBeVisible();

    await adminPage.locator('input[name="code"]').fill("code1");
    await adminPage
      .locator('input[name="statementOfCompetency"]')
      .fill("creation test");

    // exclusive checkboxes
    await adminPage.locator('input[name="isMandatory"]').check();
    await expect(adminPage.locator('input[name="isMandatory"]')).toBeChecked();
    await expect(
      adminPage.locator('input[name="isOptionnal"]')
    ).not.toBeChecked();

    await adminPage.locator('input[name="isOptionnal"]').check();
    await expect(
      adminPage.locator('input[name="isMandatory"]')
    ).not.toBeChecked();
    await expect(adminPage.locator('input[name="isOptionnal"]')).toBeChecked();
    // end exclusive checkboxes

    const [response] = await Promise.all([
      adminPage.waitForResponse(
        (response) =>
          response.url().includes("/competency") &&
          response.request().method() === "POST"
      ),
      adminPage.getByTestId("submit-competency").first().click(),
    ]);

    await expect(response.status()).toBe(201);
    await expect(adminPage.locator(".modal-overlay")).toBeHidden();
    await expect(adminPage.getByRole('cell', { name: 'code1' })).toBeVisible();
  });

  test("Creating a valid detailed ministerial competency", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programme/Seededprogram/competence/code1");
    await adminPage.waitForLoadState("networkidle");
    await adminPage.getByTestId("edit-button").first().click();

    // creation
    for (let parentIndex = 0; parentIndex < 3; parentIndex++) {
      // realisation contexts complementary information
      await adminPage.getByTestId('add-realisation-context').click();
      await adminPage.locator(`input[name="competency\\.realisationContexts\\[${parentIndex}\\]\\.value"]`).fill(`realisation context ${parentIndex + 1}`);

      // competency elements, performance criteria and complementary information
      await adminPage.getByTestId('add-competency-element').click();
      await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.value"]`).fill(`competency element ${parentIndex + 1}`);
      for (let pcIndex = 0; pcIndex < 3; pcIndex++) {
        await adminPage.getByTestId(`add-performance-criteria-${parentIndex}`).click();
        await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.performanceCriterias\\[${pcIndex}\\]\\.value"]`).fill(`performance criteria ${pcIndex + 1} for element ${parentIndex + 1}`);
      }
    }

    const [response] = await Promise.all([
      adminPage.waitForResponse(
        (response) =>
          response.url().includes("/code1") &&
          response.request().method() === "PUT"
      ),
      adminPage.getByTestId("submit-draft-button").first().click(),
    ]);

    // expectations
    await expect(response.status()).toBe(200);
    await expect(adminPage.locator(".modal-overlay")).toBeHidden();

    for (let parentIndex = 1; parentIndex <= 3; parentIndex++) {
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for rc 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for competency element 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for pc 1 for element 1`)).toBeTruthy();
      
      for (let pcIndex = 1; pcIndex <= 3; pcIndex++) {
        await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }

    // reload page to verify persistence
    await adminPage.goto("/administration/programme/Seededprogram/competence/code1");
    for (let parentIndex = 1; parentIndex <= 3; parentIndex++) {
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      
      for (let pcIndex = 1; pcIndex <= 3; pcIndex++) {
        await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }
  });

  test("updating number 1, removing number 2, keeping number 3, adding number 4", async ({
    adminPage,
  }) => {
    
    await adminPage.goto("/administration/programme/Seededprogram/competence/code1");
    await adminPage.waitForLoadState("networkidle");
    await adminPage.getByTestId("edit-button").first().click();
    
    // realisation contexts
    await adminPage.getByTestId('delete-realisation-context-button-1').click();

    // competency elements
    await adminPage.getByTestId('delete-competency-element-button-1').click();
    // performance criterias
    await adminPage.getByTestId('delete-performance-criteria-button-0-1').click();
    await adminPage.getByTestId('delete-performance-criteria-button-1-1').click();

    // adding a number 4
    const parentIndex = 2;
    // realisation contexts complementary information
    await adminPage.getByTestId('add-realisation-context').click();
    await adminPage.locator(`input[name="competency\\.realisationContexts\\[${parentIndex}\\]\\.value"]`).fill(`realisation context ${parentIndex + 2}`);

    // competency elements, performance criteria and complementary information
    await adminPage.getByTestId('add-competency-element').click();
    await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.value"]`).fill(`competency element ${parentIndex + 2}`);
    for (let pcIndex = 0; pcIndex < 3; pcIndex++) {
      await adminPage.getByTestId(`add-performance-criteria-${parentIndex}`).click();
      const displayedPcIndex = pcIndex > 0 ? pcIndex + 2 : pcIndex + 1;
      await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.performanceCriterias\\[${pcIndex}\\]\\.value"]`).fill(`performance criteria ${displayedPcIndex} for element ${parentIndex + 2}`);
    }
    // validation before submit
    for (let parentIndex = 1; parentIndex <= 4; parentIndex++) {
      // skipping number 2
      if (parentIndex === 2) continue;
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      
      for (let pcIndex = 1; pcIndex <= 4; pcIndex++) {
        if (parentIndex === 2) continue;
          await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }


    const [response] = await Promise.all([
      adminPage.waitForResponse(
        (response) =>
          response.url().includes("/code1") &&
          response.request().method() === "PUT"
      ),
      adminPage.getByTestId("submit-draft-button").first().click(),
    ]);

    await expect(response.status()).toBe(200);

    // validation after submit
    for (let parentIndex = 1; parentIndex <= 4; parentIndex++) {
      // skipping number 2
      if (parentIndex === 2) continue;
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      
      for (let pcIndex = 1; pcIndex <= 4; pcIndex++) {
        if (parentIndex === 2) continue;
        await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }
  });

  test("CRUD complementary informations", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programme/Seededprogram/competence/code1");
    await adminPage.waitForLoadState("networkidle");

    // first add
    await adminPage.getByText("realisation context 1").locator('.add-comment-btn').click();
    await adminPage.locator('textarea').first().fill('realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(await adminPage.locator('.comment-text')).toHaveText('realisationContext1 complementary information 1');
    await expect(await adminPage.locator('.comment-author')).toHaveText('TestAdmin');
    await expect(await adminPage.locator('textarea')).not.toBeVisible();
    
    // second information
    await adminPage.getByText("realisation context 1").locator('.add-comment-btn').click();
    await adminPage.locator('textarea').first().fill('second realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(await adminPage.locator('textarea')).not.toBeVisible();

    // updating first information
    await adminPage.getByText("realisationContext1 complementary information 1").first().hover();
    await adminPage.locator(".btn-edit").first().click();
    await adminPage.locator('textarea').first().fill('updated realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(await adminPage.locator('.comment-text').first()).toHaveText('updated realisationContext1 complementary information 1');

    // deleting first information
    await adminPage.getByText("updated realisationContext1 complementary information 1").first().hover();
    await adminPage.locator('.comment .btn-delete').first().click();
    await expect(adminPage.locator('.comment-text')).toHaveCount(1);  

    // adding information for competency element and performance criteria
    await adminPage.getByText('competency element 1').hover();
    await adminPage.locator('.competencies .add-comment-btn').first().click();
    await adminPage.locator('textarea').first().fill('competency element 1');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(await adminPage.locator('.comment-text').nth(1)).toHaveText('competency element 1');
    await expect(await adminPage.locator('textarea')).not.toBeVisible();
    await expect(adminPage.locator('.comment-text')).toHaveCount(2);

    await adminPage.locator('.competencies .criterion .add-comment-btn').first().click();
    await adminPage.locator('textarea').first().fill('performance criteria 1');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(await adminPage.locator('.comment-text').nth(2)).toHaveText('performance criteria 1');
    await expect(await adminPage.locator('textarea')).not.toBeVisible();
    await expect(adminPage.locator('.comment-text')).toHaveCount(3);
  });

  test("empty complementary information should not submit", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programme/Seededprogram/competence/code1");
    await adminPage.waitForLoadState("networkidle");

    // first add failing.
    await adminPage.getByText("realisation context 1").locator('.add-comment-btn').click();
    adminPage.getByTestId("submit-draft-button").first().click()
    await expect(adminPage.locator('.error-message')).toBeVisible();

    // now success
    await adminPage.locator('textarea').first().fill('realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-draft-button").first().click();

    // testing update form now
    await adminPage.getByText("realisationContext1 complementary information 1").first().hover();
    await adminPage.locator(".btn-edit").first().click();
    await adminPage.locator('textarea').first().fill('');
    await adminPage.getByTestId("submit-draft-button").first().click();
    await expect(adminPage.locator('.error-message')).toBeVisible();
  });


});
