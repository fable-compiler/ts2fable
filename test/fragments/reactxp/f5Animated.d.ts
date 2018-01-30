import Types = require('./f5Types');
export declare abstract class Animation {
    abstract start(onEnd?: Types.Animated.EndCallback): void;
}