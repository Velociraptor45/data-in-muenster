# Münster-Data

Contains the code for the website behind <a href="https://data-münster.de">data-münster.de</a> that aims to visualize open data provided by the city of Münster.

## Local build

For building the application locally, you need to download the data sets from Münster's OpenData website on your own as they are not included in the repository. Place the following files under `src\MuensterData.Infrastructure\data`:

- <a href="https://opendata.stadt-muenster.de/dataset/unf%C3%A4lle-im-regierungsbezirk-m%C3%BCnster">unfaelle-muenster.csv</a>

You also need to request a license key from <a href="https://www.syncfusion.com/">Syncfusion</a> in order to use the component framework that's utilized for data visualization. Place the key under the name `SfLicenseKey` on root-level either in the appsettings.json (dangerous) or in the project's user secrets (recommended).