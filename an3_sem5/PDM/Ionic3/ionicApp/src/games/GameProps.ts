export interface GameProps {
    _id?: string;
    title: string;
    launchDate: Date;
    platform:string;
    lastVersion:string;
    url:string;  
    authors:string[];
    totalReleases:number;
    isOpenSource:boolean;
    isNotSaved?:boolean;
    latitude?:number;
    longitude?:number;
    webViewPath?: string;
}
