import './polyfills';

import { NgModuleRef, enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';

type windowExtended = Window & typeof globalThis & {
  ngRef: NgModuleRef<AppModule>;
}

platformBrowserDynamic().bootstrapModule(AppModule).then(ref => {
  const _window = window as windowExtended;
  // Ensure Angular destroys itself on hot reloads.
  if (_window['ngRef']) {
    _window['ngRef'].destroy();
  }

  _window['ngRef'] = ref;

  // Otherwise, log the boot error
}).catch(err => console.error(err));
