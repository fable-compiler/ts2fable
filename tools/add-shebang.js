// https://www.npmjs.com/package/prepend-file
var prependFile = require('prepend-file');
prependFile('./dist/ts2fable.js', '#!/usr/bin/env node\n')