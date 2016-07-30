/**
 * Copyright (c) 2010 Appcoast Ltd. All rights reserved.
 */

// XXX: we can't use Cu here because it's used on several types of overlays and
// we get undeclared/redeclared errors if we try to use it.
Components.utils.import("resource://addoninstaller/common.js");

/**
 * AddonInstallerChrome namespace.
 */
if ("undefined" == typeof(AddonInstallerChrome)) {
  AddonInstallerChrome = {};
};
