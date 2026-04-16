declare global {
    interface GlobalThis {
        QuerieURLFromObject: (querie: { [key: string]: any }) => string;
    }

    interface Window {
        __REDUX_DEVTOOLS_EXTENSION__?: () => any;
    }
}
export {};
  