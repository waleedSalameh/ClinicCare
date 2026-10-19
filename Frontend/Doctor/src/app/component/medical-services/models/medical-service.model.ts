export interface MedicalServiceModel {
  id: number;
  serviceName: string;
  serviceCode: string;
  price: number;
  desription: string;
  isActive: number;
  isDeleted: boolean;
  durationInMinutes:number;
  costPrice:number;
  sellingPrice:number;
  clincid:number;
}
