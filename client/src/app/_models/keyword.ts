export interface Keyword {
    id: number,
    keywordName: string,
    googleHost: string,
    language: string,
    country: string,
    city: string,
    lastPosition: any,
    lastSearch: Date,
    projectId: number,
    // project: {
    //     id: number,
    //     projectName: string,
    //     domain: string,
    //     dateAdded: Date
    // }
}