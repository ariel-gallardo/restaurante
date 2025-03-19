export default class MapButtonAddEventData {
    callback: () => void;
    name: string;
    width: string;
    height: string;
    classes: string;
    iconUrl: string;

    constructor(
        callbackData: () => void,
        name: string,
        width: string,
        height: string,
        classes: string,
        iconUrl: string
    ) {
        this.callback = callbackData;
        this.name = name;
        this.width = width;
        this.height = height;
        this.classes = classes;
        this.iconUrl = iconUrl;
    }
}
