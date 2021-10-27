it('renders application', () => {
  cy.visit("/");
  cy.get(".navbar-nav li a[href*='/counter']").click();
  cy.get("#increment button").click();

  cy.get(".navbar-nav li a[href='/']").click();
  cy.get(".navbar-nav li a[href*='/counter']").click();
  cy.get("#increment p").last().should('have.text', 'Current count: 1')
});