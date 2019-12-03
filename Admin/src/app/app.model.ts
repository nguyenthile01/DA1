export interface Employer {
  result: {
    totalCount: 0;
    items: [];
  };
  id: number;
  name: string;
  nameCompany: string;
  userName: string;
  emailAddress: string;
  phoneNumber: string;
  password: string;
  address: string;
}
export interface IJobSeeker {
  result: {
    totalCount: 0;
    items: [];
  };
  surName: string;
  middleName: string;
  name: string;
  userName: string;
  emailAddress: string;
  password: string;
  phoneNumber: string;
  address: string;
  avtarUrl: string;
  professionalTitle: string;
  yearsOfExperience: number;
  id: number;
}
export interface ICity {
  result: {
    totalCount: 0;
    items: [];
  };
  name: string;
  id: number;
  creationTime: Date;
  isActive: boolean;
  displayOrder: number;
}
export interface IJobCategory {
  result: {
    totalCount: 0;
    items: [];
  };
  name: string;
  id: number;
  creationTime: Date;
  isActive: boolean;
  displayOrder: number;
}
export interface IExperience {
  result: {
    totalCount: 0;
    items: [];
    id: number;
    title: string;
    company: string;
    isCurrentJob: true;
    dateFrom: Date;
    dateTo: Date;
    description: string;
    jobSeekerId: number;
  };
}
export interface IKnowledge {
  result: {
    totalCount: 0;
    items: [];
    id: number;
    specialized: string;
    school: string;
    qualification: string;
    dateFrom: Date;
    dateTo: Date;
    achievement: string;
    jobSeekerId: number;
  };
}
export interface IDesiredCareer {
  result: {
    totalCount: 0;
    items: [];
  };
  id: number;
  jobSeekerId: number;
  jobCategoryId: number;
  jobCategoryName: string;
}
export interface IDesiredLocationJob {
  result: {
    totalCount: 0;
    items: [];
  };
  id: number;
  jobSeekerId: number;
  cityId: number;
  citiesName: string;
}
export interface IJob {
  result: {
    totalCount: 0;
    items: [];
  };
  id: number;
  categoryId: number;
  categoryName: number;
  amountOfPeople: number;
  title: string;
  rankAtWork: string;
  wage: number;
  cityId: number;
  employerId: number;
  descJob: string;
  workExperience: string;
  degree: string;
  gender: string;
  deadlineForSubmission: Date;
  profileLanguage: string;
  jobRequirement: string;
  isActive: true;
}
