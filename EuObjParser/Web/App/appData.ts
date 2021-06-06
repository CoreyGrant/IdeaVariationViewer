async function getFile(filename, folder = null) {
    if (folder) {
        filename = folder + "/" + filename;
    }
    return await fetch("Data/" + filename).then(x => x.json());
}

export {
    getFile
};