export interface ItemProps {
  id?: string;
  title: string;
  launchDate: Date;
  platform:string;
  lastVersion:string;
  url:string;  
  authors:string[];
  totalReleases:number;
  isOpenSource:boolean;
}
