import RX = require('./f4Interface');
export declare type SyntheticEvent = React.SyntheticEvent<any>;
export interface LinkProps
{
    onPress?: (e: RX.Types.SyntheticEvent, url: string) => void;
}