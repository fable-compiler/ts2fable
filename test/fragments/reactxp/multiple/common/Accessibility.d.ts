/**
* Accessibility.ts
*
* Copyright (c) Microsoft Corporation. All rights reserved.
* Licensed under the MIT license.
*
* Common wrapper for accessibility helper exposed from ReactXP.
*/
import SubscribableEvent from 'subscribableevent';
export declare abstract class Accessibility {
    abstract isScreenReaderEnabled(): boolean;
    screenReaderChangedEvent: SubscribableEvent<(isEnabled: boolean) => void>;
    isHighContrastEnabled(): boolean;
    newAnnouncementReadyEvent: SubscribableEvent<(announcement: string) => void>;
    announceForAccessibility(announcement: string): void;
}
export default Accessibility;
