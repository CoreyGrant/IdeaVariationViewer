async function getIdeaVariation() {
    return await fetch("Data/IdeaVariation.json").then(x => x.json());
}

async function getAnbennar() {
    return await fetch("Data/Anbennar.json").then(x => x.json());
}

export {
    getIdeaVariation,
    getAnbennar
}
