// https://www.npmjs.com/package/prepend-file
import prependFile from "prepend-file"
prependFile('./dist/ts2fable.js', '#!/usr/bin/env node\n')