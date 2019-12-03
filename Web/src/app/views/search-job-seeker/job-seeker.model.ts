export interface ICategoryJob {
    result: {
      totalCount: 0;
      items: [];
    }
    id: number;
    name: string;
  }
  export interface ILocation {
    result:{
      totalCount: 0;
      items: [];
    }
    id: number;
    name: string;
  }

  export interface IJobSeeker{
    result:{
        totalCount: 0;
        items: [];
      }
      id: number;
      surName: string;
      middleName: string;
      name: string;
      emailAddress: string;
      phoneNumber: string;
      avtarUrl: string;
  }