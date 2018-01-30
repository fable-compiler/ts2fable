import RXTypes = require('reactxp');
import AnimatedImpl = require('reactxp');

declare module ReactXP {
    export import CommonStyledProps = RXTypes.CommonStyledProps;
    export import Animated = AnimatedImpl;
}
