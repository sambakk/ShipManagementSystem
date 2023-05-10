# Ship Management Frontend

Small frontend PoC of a ship management system.

# Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Available Scripts](#available-scripts)
- [Dependencies](#dependencies)
- [Author](#author)

# Installation

To install the project, follow these steps:

1. Make sure you have Node.js installed on your local machine.
2. Clone the repository:

```bash
git clone https://github.com/sambakk/ShipManagementSystem.git
cd ShipManagementSystem/ShipManagementFront
yarn install
```

# Usage

To start the project, run the following command:

```bash
yarn start
```

The application will be available at http://localhost:3000.

# End-to-end Testing

**Note**: Ensure the backend is operational prior to conducting end-to-end testing, as this will provide the necessary data for the frontend to function correctly.

Run the following command to run end-to-end tests using Cypress in headless mode.

```bash
yarn test:e2e
```

Run the following command to open the Cypress GUI for running end-to-end tests interactively.

```bash
yarn test:e2e-gui
```

[![Cypress E2E Testing Demo](https://i.ibb.co/drVRSGf/e2e.png)](https://player.vimeo.com/video/825240108?autoplay=true)


# Author
Abdessamad Bakkach
