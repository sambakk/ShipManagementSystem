import { generateRandomString } from "../../src/utils/helpers";
import { Ship } from "../../src/types/Ship";
describe('User Journey in Ship Operations', () => {

  beforeEach(() => {
    // Visit the page containing the table
    cy.visit('/');
  });

  it("Show validation errors in add a new ship", () => {

    // Click o add a new chip
    cy.contains(/ADD NEW SHIP/i).click();

    // Validation for code
    cy.get('input[name="code"]').type("ZD76").blur();
    cy.get('p').contains('Code format is not correct (e.g. AAAA-1111-A1)');

    cy.get('input[name="code"]').clear().focus().invoke('val', '').blur();
    cy.get('p').contains('Code format is not correct (e.g. AAAA-1111-A1)');

    // Validation for name
    cy.get('input[name="name"]').focus().invoke('val', '').blur();
    cy.get('p').contains('Name is required');

    // Validation for length
    cy.get('input[name="length"]').type("0").blur();
    cy.get('p').contains('Length is not valid');

    cy.get('input[name="length"]').clear().type("-54").blur();
    cy.get('p').contains('Length is not valid');

    // Validation for width
    cy.get('input[name="width"]').type("-54").blur();
    cy.get('p').contains('Width is not valid');

    cy.get('input[name="width"]').clear().type("0").blur();
    cy.get('p').contains('Width is not valid');

  });

  it('Add a new ship', () => {

    // Add a new ship
    const ship = <Ship>{
      code: generateRandomString(),
      name: 'OOCL Hong Kong',
      length: 399.87,
      width: 58.8
    }

    // Click o add a new chip
    cy.contains(/ADD NEW SHIP/i).click();

    // Input valid data
    cy.get('input[name="code"]').type(ship.code);
    cy.get('input[name="name"]').type(ship.name);
    cy.get('input[name="length"]').type(ship.length.toString());
    cy.get('input[name="width"]').type(ship.width.toString());
    cy.contains(/Submit/i).click();

    // If the new row is inserted in the next page, go to next page
    cy.get(`[aria-label="Go to next page"]`).click({ force: true })

    // Check if new ship is added
    cy.contains(ship.code)
      .parent('tr')
      .within(() => {
        cy.get('td').eq(2).contains(ship.name)
        cy.get('td').eq(3).contains(ship.length)
        cy.get('td').eq(4).contains(ship.width)
      });

  });


  it('Show validation errors in update ship', () => {

    // Check if there's records to delete in the table
    cy.get('table tbody').then(($table) => {

      if (!$table.text().includes("No records to display")) {

        // Select first element in the table & click on edit
        cy.get('table tbody > tr:first-child').within(() => {
          cy.get('td > div > button:first-child').click();

        });

        // Validation for code
        cy.get('input[name="code"]').clear().type("ZD76").blur();
        cy.get('p').contains('Code format is not correct (e.g. AAAA-1111-A1)');

        cy.get('input[name="code"]').clear().focus().invoke('val', '').blur();
        cy.get('p').contains('Code format is not correct (e.g. AAAA-1111-A1)');

        // Validation for name
        cy.get('input[name="name"]').focus().invoke('val', '').blur();
        cy.get('p').contains('Name is required');

        // Validation for length
        cy.get('input[name="length"]').clear().type("0").blur();
        cy.get('p').contains('Length is not valid');

        cy.get('input[name="length"]').clear().type("-54").blur();
        cy.get('p').contains('Length is not valid');

        // Validation for width
        cy.get('input[name="width"]').clear().type("-54").blur();
        cy.get('p').contains('Width is not valid');

        cy.get('input[name="width"]').clear().type("0").blur();
        cy.get('p').contains('Width is not valid');
      }
    });

  });

  it('Update a ship', () => {

    // Check if there's records to delete in the table
    cy.get('table tbody').then(($table) => {

      if (!$table.text().includes("No records to display")) {

        // Add a new ship
        const ship = <Ship>{
          code: generateRandomString(),
          name: 'OOCL H.K.',
          length: 387.44,
          width: 49.9
        }

        // Select first element in the table & click on edit
        cy.get('table tbody > tr:first-child').within(() => {
          cy.get('td > div > button:first-child').click();

        });

        // Input valid edit data
        cy.get('input[name="code"]').clear().type(ship.code);
        cy.get('input[name="name"]').clear().type(ship.name);
        cy.get('input[name="length"]').clear().type(ship.length.toString());
        cy.get('input[name="width"]').clear().type(ship.width.toString());
        cy.contains(/Save/i).click();

        // Check if new ship is updated
        cy.contains(ship.code)
          .parent('tr')
          .within(() => {
            cy.get('td').eq(2).contains(ship.name)
            cy.get('td').eq(3).contains(ship.length)
            cy.get('td').eq(4).contains(ship.width)
          });

      }
    });

  });

  it('Delete a ship', () => {

    // Check if there's records to delete in the table
    cy.get('table tbody').then(($table) => {

      if (!$table.text().includes("No records to display")) {

        // Select first element in the table & click on delete
        cy.get('table tbody > tr:first-child > td:nth-child(2)').invoke('text').then(($val) => {
          cy.log($val);
          cy.get('table tbody > tr:first-child > td > div > button:nth-child(2)').click();
          cy.get('table').should('not.include.text', $val);
        });

      }
    })
  });

})