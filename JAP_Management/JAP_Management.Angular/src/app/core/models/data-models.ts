import { DecimalPipe } from "@angular/common";

export class StudentModel{
    baseUserId:string="";
    fullName:string="";
    mentorName:string="";
    programName:string="";
    selectionName:string="";
    studentStatusName:string="";
    commentByUser:string="";
    items:Array<StudentItemsModel>=new Array<StudentItemsModel>();
}

export class StudentItemsModel{
    itemName:string="";
    url:string="";
    expectedHours:number=0;
    percentageDone:string="";
    candidateStatus:string="";
    startDate:Date;
    endDate:Date;
    orderNumber:number=0;
}

export class StudentUpsertRequest{
    baseUserId:string="";
    firstName:string="";
    lastName:string="";
    email:string="";
    mentorId:number=0;
    selectionId:number=0;
    programId:number=0;
    studentStatusId:number=0;
    dateOfBirth:Date=new Date();
    //commentByUser:string="";
}
export class SelectionUpsertRequest{
    id:number=0;
    selectionName:string="";
    statusId:number=0;
    programId:number=0;
    year:string="";

}
export class SelectionModel{
    id:number=0;
    selectionName:string="";
    statusName:string="";
    year:string="";
    programName:string="";
    students:Array<StudentModel>=new Array<StudentModel>();
}

export class ProgramModel{
    id:number=0;
    name:string="";
    description:string="";
    technologies:Array<TechnologiesModel>=new Array<TechnologiesModel>();
}

export class TechnologiesModel{
    id:number=0;
    name:string="";
}

export class LoginModel{
    username:string="";
    password:string="";
}

export class SearchModel{
    
    value:string="";
}

export class StudentRequestModel{
    page:number=1;
    filter:number=0;
    sorting:number=0;
    search:SearchModel=new SearchModel();
}
export class ProgramRequestModel{
    search:SearchModel=new SearchModel();
}
export class SelectionRequestModel{
    page:number=1;
    filter:number=0;
    sorting:number=0;
    search:SearchModel=new SearchModel();
}

export class RankModel{
    selectionName:string="";
    programName:string="";
    studentSuccessRate:DecimalPipe;
    overallSuccess:number=0;
}

export class ItemsModel{
    id:number=0;
    name:string="";
    description:string="";
    url:string="";
    expectedHours:number=0;
    orderNumber:number=0;
}

export class ProgramItemsRequestModel{
    page:number=1;
    filter:number=0;
    sorting:number=0;
    search:SearchModel=new SearchModel();
}

export class ProgramItemModel{
    programId:number=0;
    itemId:number=0;
    itemsModel:Array<ItemsModel>=new Array<ItemsModel>();
}
