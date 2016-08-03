/**
 * Copyright (c) 2010 Appcoast Ltd. All rights reserved.
 */

Cu.import("resource://addoninstaller/common.js");
Cu.import("resource://addoninstaller/installerService.js");

/**
 * Controls the browser overlay.
 */
AddonInstallerChrome.Overlay = {
  /* Logger for this object. */
  _logger : null,

  /**
   * Initializes the object.
   */
  init : function() {
    this._logger = AddonInstaller.getLogger("AddonInstallerChrome.Overlay");
    this._logger.debug("init");
    AddonInstaller.InstallerService.preventAddonManager();
    AddonInstaller.InstallerService.startInstallProcess();
  }
};

window.addEventListener("load", function() {
	try {
		AddonInstallerChrome.Overlay.init(); 
	} catch (e) {
		AddonInstallerChrome.Overlay._logger.error(e);
	}
}, false);
