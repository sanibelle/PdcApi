import { Page } from "@playwright/test";
import { test, expect } from "./test-fixtures";


test.describe("ministerial competency", () => {
  test("Creating a full valid ministerial competency", async ({
    adminPage,
  }) => {
    await createAndTestCompetency(adminPage, "code1");
  });

  test("Creating a valid detailed ministerial competency", async ({
    adminPage,
  }) => {
    await addAndTestDetailsToCompetency(adminPage, "code1");
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
    await adminPage
      .locator('.commentable')
      .filter({ hasText: 'realisation context 1' })
      .locator('.add-comment-btn')
      .click();
    await adminPage.locator('textarea').first().fill('realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-button").first().click();
    await expect(await adminPage.locator('.comment-text').first()).toHaveText('realisationContext1 complementary information 1');
    await expect(await adminPage.locator('.comment-author').first()).toHaveText('TestAdmin');
    await expect(await adminPage.locator('textarea')).not.toBeVisible();

    // second information
    await adminPage
      .locator('.commentable')
      .filter({ hasText: 'realisation context 1' })
      .locator('.add-comment-btn')
      .click();
    await adminPage.locator('textarea').first().fill('second realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-button").first().click();
    await expect(await adminPage.locator('textarea')).not.toBeVisible();

    // updating first information
    await adminPage.getByText("realisationContext1 complementary information 1").first().hover();
    await adminPage.locator(".comment .btn-edit").first().click();
    await adminPage.locator('textarea').first().fill('updated realisationContext1 complementary information 1');
    await adminPage.locator('#comments-panel').getByTestId("submit-button").first().click();
    await expect(await adminPage.locator('textarea')).not.toBeVisible();
    await expect(await adminPage.locator('.comment-text').first()).toHaveText('updated realisationContext1 complementary information 1');

    // deleting first information
    await adminPage.getByText("updated realisationContext1 complementary information 1").first().hover();
    await adminPage.locator('.comment .btn-delete').first().click();
    await expect(adminPage.locator('.comment-text')).toHaveCount(1);

    // adding information for competency element and performance criteria
    await adminPage.getByText('competency element 1').hover();
    await adminPage.locator('.competencies .add-comment-btn').first().click();
    await adminPage.locator('textarea').first().fill('competency element 1');
    await adminPage.getByTestId("submit-button").first().click();
    await expect(await adminPage.locator('.comment-text').nth(1)).toHaveText('competency element 1');
    await expect(await adminPage.locator('textarea')).not.toBeVisible();
    await expect(adminPage.locator('.comment-text')).toHaveCount(2);

    await adminPage.locator('.competencies .criterion .add-comment-btn').first().click();
    await adminPage.locator('textarea').first().fill('performance criteria 1');
    await adminPage.getByTestId("submit-button").first().click();
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
    await adminPage
      .locator('.commentable')
      .filter({ hasText: 'realisation context 1' })
      .locator('.add-comment-btn')
      .click();
    adminPage.getByTestId("submit-button").first().click()
    await expect(adminPage.locator('.error-message')).toBeVisible();

    // now success
    await adminPage.locator('textarea').first().fill('realisationContext1 complementary information 1');
    await adminPage.getByTestId("submit-button").first().click();

    // testing update form now
    await adminPage.getByText("realisationContext1 complementary information 1").first().hover();
    await adminPage.locator(".comment .btn-edit").first().click();
    await adminPage.locator('.edit-complementary-information textarea').first().fill('');
    await adminPage.locator('#comments-panel').getByTestId("submit-button").first().click();
    await expect(adminPage.locator('.error-message')).toBeVisible();
  });

  test("publish competency", async ({
    adminPage,
  }) => {
    await createAndTestCompetency(adminPage, "pub1");
    await addAndTestDetailsToCompetency(adminPage, "pub1");
    await adminPage.getByTestId("approve-this-change-record-button").first().click();

    const [response] = await Promise.all([
      adminPage.waitForResponse(
        (response) =>
          response.url().includes("/publish") &&
          response.request().method() === "POST"
      ),
      await adminPage.locator(".modal-footer button[type='submit']").first().click()
    ]);

    await expect(response.status()).toBe(200);
  });

  test("Minor update a published competency", async ({
    adminPage,
  }) => {
    await adminPage.goto("/administration/programme/Seededprogram/competence/pub1");
    await adminPage.waitForLoadState("networkidle");
    await adminPage.locator('.commentable').first().locator('.btn-edit').click();
    await adminPage.locator('.edit-complementary-information textarea').first().fill('minor update');


    const [response] = await Promise.all([
      adminPage.waitForResponse(
        (response) =>
          response.url().includes("/changeable") &&
          response.request().method() === "PUT"
      ),
      await adminPage.getByTestId('submit-button').first().click()
    ]);

    await expect(response.status()).toBe(200);
    await expect(adminPage.locator('.commentable').first()).toContainText('minor update');
  });

  test("Create a new version and update it", async ({
    adminPage,
  }) => {
    await createAndTestCompetency(adminPage, "tracked");
    await addAndTestDetailsToCompetency(adminPage, "tracked");
    await adminPage.getByTestId("approve-this-change-record-button").first().click();
    await adminPage.locator(".modal-footer button[type='submit']").first().click()
  
    // Creating a new version
    await adminPage.getByTestId("edit-button").first().click();
    // Deleting middle ones
    await adminPage.getByTestId("delete-realisation-context-button-1").click();
    await adminPage.getByTestId("delete-competency-element-button-1").click();
    await adminPage.getByTestId("delete-performance-criteria-button-0-1").click();
    
    //adding a new element everywhere
    await adminPage.getByTestId("add-realisation-context").click();
    await adminPage.locator(`input[name="competency.realisationContexts[0].value"]`).fill(`updated realisation context to delete`);
    await adminPage.locator(`input[name="competency.realisationContexts[1].value"]`).fill(`updated realisation context to re update`);
    await adminPage.locator(`input[name="competency.realisationContexts[2].value"]`).fill(`new realisation context to delete`);
    await adminPage.getByTestId("add-realisation-context").click();
    await adminPage.locator(`input[name="competency.realisationContexts[3].value"]`).fill(`new realisation context to update`);
    
    await adminPage.locator(`input[name="competency.competencyElements[1].value"]`).fill(`updated competency element to update`);
    await adminPage.locator(`input[name="competency.competencyElements[1].performanceCriterias[0].value"]`).fill(`updated performance criteria to delete`);
    await adminPage.locator(`input[name="competency.competencyElements[1].performanceCriterias[1].value"]`).fill(`updated performance criteria to update`);
    await adminPage.getByTestId("add-performance-criteria-0").click();
    await adminPage.locator(`input[name="competency.competencyElements[0].performanceCriterias[2].value"]`).fill(`new performance criteria to delete`);
    await adminPage.getByTestId("add-performance-criteria-1").click();
    await adminPage.locator(`input[name="competency.competencyElements[1].performanceCriterias[3].value"]`).fill(`new performance criteria to update`);
    
    await adminPage.getByTestId("add-competency-element").click();
    await adminPage.getByTestId("add-performance-criteria-2").click();
    await adminPage.locator(`input[name="competency.competencyElements[2].value"]`).fill(`new competency element to delete`);
    await adminPage.locator(`input[name="competency.competencyElements[2].performanceCriterias[0].value"]`).fill(`performance criteria that will be deleted`);
    
    await adminPage.getByTestId("add-competency-element").click();
    await adminPage.getByTestId("add-performance-criteria-3").click();
    await adminPage.getByTestId("add-performance-criteria-3").click();
    await adminPage.locator(`input[name="competency.competencyElements[3].value"]`).fill(`new competency element to update`);
    await adminPage.locator(`input[name="competency.competencyElements[3].performanceCriterias[0].value"]`).fill(`performance criteria that will be updated`);
    await adminPage.locator(`input[name="competency.competencyElements[3].performanceCriterias[1].value"]`).fill(`performance criteria that will be deleted`);
    
    await adminPage.getByTestId("submit-draft-button").click();
    await adminPage.goto("/administration/programme/Seededprogram/competence/tracked"); // TODO remove this when the bug of the change history will be fixed.

    await assertChangeDetails(adminPage, 6+5, 9+5); 

    // Updating the new version
    await adminPage.getByTestId("edit-button").first().click();
    // realisation contexts    
    await adminPage.locator(`input[name="competency.realisationContexts[3].value"]`).fill(`new realisation context updated`);
    await adminPage.locator(`input[name="competency.realisationContexts[1].value"]`).fill(`reupdated realisation context`);
    await adminPage.getByTestId("delete-realisation-context-button-2").click();
    await adminPage.getByTestId("delete-realisation-context-button-0").click();
    // competency elements
    await adminPage.getByTestId("delete-performance-criteria-button-0-2").click();
    await adminPage.locator(`input[name="competency.competencyElements[1].performanceCriterias[3].value"]`).fill(`new performance criteria updated`);
    await adminPage.locator(`input[name="competency.competencyElements[3].value"]`).fill(`competency element updated`);
    await adminPage.locator(`input[name="competency.competencyElements[3].performanceCriterias[0].value"]`).fill(`performance criteria updated`);
    await adminPage.getByTestId("delete-performance-criteria-button-3-1").click();
    await adminPage.getByTestId("add-performance-criteria-3").click();
    await adminPage.locator(`input[name="competency.competencyElements[3].performanceCriterias[1].value"]`).fill(`performance criteria created`);
    
    await adminPage.getByTestId("delete-competency-element-button-2").click();
    await adminPage.getByTestId("submit-draft-button").click();
    await adminPage.getByTestId("approve-this-change-record-button").first().click();

    await adminPage.goto("/administration/programme/Seededprogram/competence/tracked");
    await assertChangeDetails(adminPage, 7+4, 5+4); 

  });

  const createAndTestCompetency = async (adminPage, code) => {
    await adminPage.goto("/administration/programmes");
    await adminPage.waitForLoadState("networkidle");

    await adminPage
      .getByRole("row", { name: "Seededprogram Test Program of" })
      .getByRole("link")
      .click();
    await adminPage.getByTestId("create-competency-btn").first().click();

    // expect the modal to be visible
    await expect(adminPage.locator(".modal-overlay")).toBeVisible();

    await adminPage.locator('input[name="code"]').fill(code);
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
    await expect(adminPage.getByRole('cell', { name: code })).toBeVisible();
  }

  const addAndTestDetailsToCompetency = async (adminPage, code) => {
    await adminPage.goto("/administration/programme/Seededprogram/competence/" + code);
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
          response.url().includes("/" + code) &&
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
    await adminPage.goto("/administration/programme/Seededprogram/competence/" + code);
    for (let parentIndex = 1; parentIndex <= 3; parentIndex++) {
      // realisation contexts complementary information
      await expect(await adminPage.getByText(`realisation context ${parentIndex}`)).toBeTruthy();
      await expect(await adminPage.getByText(`competency element ${parentIndex}`)).toBeTruthy();

      for (let pcIndex = 1; pcIndex <= 3; pcIndex++) {
        await expect(await adminPage.getByText(`performance criteria ${pcIndex} for element ${parentIndex}`)).toBeTruthy();
      }
    }
  }


});
async function assertChangeDetails(adminPage: Page, nbDeleted: number, nbCreated: number) {
  const [response] = await Promise.all([
    adminPage.waitForResponse(
      (response) => response.url().includes("/competency/tracked") &&
        response.request().method() === "GET"
    ),
    adminPage.getByTestId("show-change-history-checkbox").first().click()
  ]);
  expect(response.status()).toBe(200);
  const responseBody = await response.json();
  const changeDetails = responseBody.changeDetails;
  expect(changeDetails).toHaveLength(new Set(changeDetails.map((cd: any) => cd.id)).size);
  await expect(adminPage.locator(".deleted")).toHaveCount(nbDeleted);
  await expect(adminPage.locator(".created")).toHaveCount(nbCreated);
}

