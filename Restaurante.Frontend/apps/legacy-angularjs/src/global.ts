function QuerieURLFromObject(querie: { [key: string]: any }){
    let data = [];
    if(querie){
        data = [...data,...Object.entries(querie)
        .map(([k, v]) => (v ? [String(k), String(v)] : null))
        .filter((x) => x != null)]
    }
    return data.length > 0 ? `?${new URLSearchParams(data).toString()}` : '';
}

globalThis.QuerieURLFromObject = QuerieURLFromObject;
