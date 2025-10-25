import { test, expect } from "./test-fixtures";

test.describe("ministerial competency", () => {
  test("Creating a full valid ministerial competency", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programOfStudy");
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
  });

  test("Creating a valid detailed ministerial competency", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programOfStudy/Seededprogram/competency/code1");
    await adminPage.waitForLoadState("networkidle");

    // creation
    for (let parentIndex = 0; parentIndex < 3; parentIndex++) {
      // realisation contexts complementary information
      await adminPage.getByTestId('add-realisation-context').click();
      await adminPage.locator(`input[name="competency\\.realisationContexts\\[${parentIndex}\\]\\.value"]`).fill(`realisation context ${parentIndex + 1}`);
      await adminPage.getByTestId('add-complementary-information-competency.realisationContexts[0]').click();
      await adminPage.locator(`input[name="competency\\.realisationContexts\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 1} for rc 1`);

      // competency elements, performance criteria and complementary information
      await adminPage.getByTestId('add-competency-element').click();
      await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.value"]`).fill(`competency element ${parentIndex + 1}`);
      for (let pcIndex = 0; pcIndex < 3; pcIndex++) {
        if (pcIndex > 0) {
          await adminPage.getByTestId(`add-performance-criteria-${parentIndex}`).click();
        }
        await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.performanceCriterias\\[${pcIndex}\\]\\.value"]`).fill(`performance criteria ${pcIndex + 1} for element ${parentIndex + 1}`);
      }

      // complementary information for first competency element
      await adminPage.getByTestId('add-complementary-information-competency.competencyElements[0]').click();
      await adminPage.locator(`input[name="competency\\.competencyElements\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 1} for competency element 1`);

      // complementary information for first performance criteria of first competency element
      await adminPage.getByTestId('add-complementary-information-competency.competencyElements[0].performanceCriterias[0]').click();
      await adminPage.locator(`input[name="competency\\.competencyElements\\[0\\]\\.performanceCriterias\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 1} for pc 1 for element 1`);
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
    await adminPage.goto("/administration/programOfStudy/Seededprogram/competency/code1");
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
  });

  test("updating number 1, removing number 2, keeping number 3, adding number 4", async ({
    adminPage,
  }) => {
    
    await adminPage.goto("/administration/programOfStudy/Seededprogram/competency/code1");
    await adminPage.waitForLoadState("networkidle");

    // realisation contexts
    await adminPage.getByTestId('delete-realisation-context-button-1').click();
    await adminPage.getByTestId('delete-complementary-information-competency.realisationContexts[0]-1').click();

    // competency elements
    await adminPage.getByTestId('delete-competency-element-button-1').click();
    // performance criterias
    await adminPage.getByTestId('delete-performance-criteria-button-0-1').click();
    await adminPage.getByTestId('delete-performance-criteria-button-1-1').click();
    // complementary information for competency element
    await adminPage.getByTestId('delete-complementary-information-competency.competencyElements[0]-1').click();
    // complementary information for performance criteria
    await adminPage.getByTestId('delete-complementary-information-competency.competencyElements[0].performanceCriterias[0]-1').click();


    // adding a number 4
    const parentIndex = 2;
    // realisation contexts complementary information
    await adminPage.getByTestId('add-realisation-context').click();
    await adminPage.locator(`input[name="competency\\.realisationContexts\\[${parentIndex}\\]\\.value"]`).fill(`realisation context ${parentIndex + 2}`);
    await adminPage.getByTestId('add-complementary-information-competency.realisationContexts[0]').click();
    await adminPage.locator(`input[name="competency\\.realisationContexts\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 2} for rc 1`);

    // competency elements, performance criteria and complementary information
    await adminPage.getByTestId('add-competency-element').click();
    await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.value"]`).fill(`competency element ${parentIndex + 2}`);
    for (let pcIndex = 0; pcIndex < 3; pcIndex++) {
      if (pcIndex > 0) {
        await adminPage.getByTestId(`add-performance-criteria-${parentIndex}`).click();
      }
      const displayedPcIndex = pcIndex > 0 ? pcIndex + 2 : pcIndex + 1;
      await adminPage.locator(`input[name="competency\\.competencyElements\\[${parentIndex}\\]\\.performanceCriterias\\[${pcIndex}\\]\\.value"]`).fill(`performance criteria ${displayedPcIndex} for element ${parentIndex + 2}`);
    }

    // complementary information for first competency element
    await adminPage.getByTestId('add-complementary-information-competency.competencyElements[0]').click();
    await adminPage.locator(`input[name="competency\\.competencyElements\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 2} for competency element 1`);

    // complementary information for first performance criteria of first competency element
    await adminPage.getByTestId('add-complementary-information-competency.competencyElements[0].performanceCriterias[0]').click();
    await adminPage.locator(`input[name="competency\\.competencyElements\\[0\\]\\.performanceCriterias\\[0\\]\\.complementaryInformations\\[${parentIndex}\\]\\.text"]`).fill(`complementary information ${parentIndex + 2} for pc 1 for element 1`);

    // validation before submit
    for (let parentIndex = 1; parentIndex <= 4; parentIndex++) {
      // skipping number 2
      if (parentIndex === 2) continue;
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for rc 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for competency element 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for pc 1 for element 1`)).toBeTruthy();
      
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
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for rc 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for competency element 1`)).toBeTruthy();
      await expect(await adminPage.getByText(`complementary information ${parentIndex} for pc 1 for element 1`)).toBeTruthy();
      
      for (let pcIndex = 1; pcIndex <= 4; pcIndex++) {
        if (parentIndex === 2) continue;
        await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }
  });
});
