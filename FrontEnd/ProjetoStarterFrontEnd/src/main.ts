import 'hammerjs';
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import {LicenseManager} from "ag-grid-enterprise";
LicenseManager.setLicenseKey("Evaluation_License-_Not_For_Production_Valid_Until_28_July_2019__MTU2NDI2ODQwMDAwMA==db32728bad1d3ee14ef344c27f0b42be")

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
