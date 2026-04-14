declare global {
    interface GlobalThis {
        QuerieURLFromObject: (querie: { [key: string]: any }) => string;
    }
}
export {};
  