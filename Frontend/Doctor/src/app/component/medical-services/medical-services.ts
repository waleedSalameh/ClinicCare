import {ChangeDetectorRef,Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import { FormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDialogModule,MatDialog} from '@angular/material/dialog';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MedicalServiceModel } from './models/medical-service.model';
import { MedicalServiceService } from './services/medical-service.service';
import { MatOption } from '@angular/material/select';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ClincModele } from '../clinc/models/clinc.models';
@Component({
  selector: 'app-medical-services',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, MatTableModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatDialogModule, MatSlideToggleModule, MatOption,MatSelectModule,MatCheckboxModule],
  templateUrl: './medical-services.html',
  styleUrl: './medical-services.css'
})
export class MedicalServicesComponent implements OnInit {
  Clincs: ClincModele[] = [];

  medicalServices: MedicalServiceModel[] = [];
  nextServiceCode: string = '';
  serviceName: string = '';
  serviceCode: string = '';
  isActive:number=-1;
  displayedColumns: string[] = ['id','serviceCode','serviceName','price','desription','durationInMinutes','costPrice','sellingPrice','isActive','actions'];

  isEditMode: boolean = false;

  selectedMedicalServiceId: number = 0;

  medicalServiceForm: FormGroup;

  errorMessage: string = '';

  @ViewChild('addMedicalServiceDialog')
  addMedicalServiceDialog!: TemplateRef<any>;

  @ViewChild('successDialog')
  successDialog!: TemplateRef<any>;

  @ViewChild('errorDialog')
  errorDialog!: TemplateRef<any>;

  @ViewChild('deleteDialog')
  deleteDialog!: TemplateRef<any>;

  @ViewChild('deleteSuccessDialog')
  deleteSuccessDialog!: TemplateRef<any>;

  @ViewChild('deleteErrorDialog')
  deleteErrorDialog!: TemplateRef<any>;


  constructor(
    private medicalService: MedicalServiceService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder
  ) {

    this.medicalServiceForm = this.fb.group({
      serviceName: [''],
      serviceCode: [{ value: '', disabled: true }],
      desription: [''],
      price: [0],
      isActive: [1],
      durationInMinutes:[0],
      costPrice:[0],
      sellingPrice:[0],
      clincid:[null,Validators.required]

    });

  }


ngOnInit(): void {

    this.loadMedicalServices();

}
loadMedicalServices(): void {

    this.medicalService.getAll(
      this.serviceName,
      this.serviceCode
    ).subscribe({

      next: (response) => {

        this.medicalServices = response;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

      }

    });
    this.getClinclist()

}
search(): void {

    this.medicalService
      .getAll(
        this.serviceName,
         this.serviceCode,
         this.isActive
      )
      .subscribe({

        next: (response) => {

          this.medicalServices = response;

          this.cdr.detectChanges();

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

        }

      });

}
clearFilters(): void {

    this.serviceName = '';

    this.serviceCode = '';
    this.isActive = -1;
    this.loadMedicalServices();

}
openAddMedicalService(): void {

    this.isEditMode = false;

    this.selectedMedicalServiceId = 0;
     let nextCode = 1;

  if (this.medicalServices.length > 0) {

    const lastCode = Math.max(
      ...this.medicalServices.map(x => Number(x.serviceCode))
    );

    nextCode = lastCode + 1;
  }

    this.medicalServiceForm.reset({

      serviceName: '',
     serviceCode: nextCode.toString(),
      desription: '',
       price: 0,
       durationInMinutes: 0,
       costPrice: 0,
       sellingPrice: 0,
       clincid:null,
      isActive: 1

    });

    this.dialog.open(
      this.addMedicalServiceDialog,
      {
        width: '600px'
      }
    );
this.getClinclist()
}
addMedicalService(): void {

  this.medicalService
    .create(this.medicalServiceForm.value)
    .subscribe({

      next: (response) => {

         if(response){
        this.loadMedicalServices();
        this.cdr.detectChanges();
        this.dialog.closeAll();

        this.dialog.open(
          this.successDialog,
          {
            width: '400px'
          }
        );}
        else{


           this.dialog.open(
          this.deleteErrorDialog);

        }

      },

     error: (error) => {

  const errors = error.error?.errors;

  if (errors) {

    const messages: string[] = [];

    Object.keys(errors).forEach(key => {

      messages.push(
        ...errors[key]
      );

    });

    this.errorMessage = messages.join('\n');

  } else {

    this.errorMessage =
      'Something went wrong.';

  }

  this.dialog.open(
    this.errorDialog,
    {
      width: '400px'
    }
  );
this.getClinclist()
}


    });

}
openEditMedicalService(id: number): void {

    this.isEditMode = true;

    this.medicalService.getById(id).subscribe({

        next: (medicalService) => {

          this.selectedMedicalServiceId =medicalService.id;

          this.medicalServiceForm.patchValue({

            serviceName:medicalService.serviceName,
            serviceCode: medicalService.serviceCode,
            desription: medicalService.desription,
            price:medicalService.price,
            isActive:medicalService.isActive,
            durationInMinutes:medicalService.durationInMinutes,
            costPrice:medicalService.costPrice,
            sellingPrice:medicalService.sellingPrice,
            clincid:medicalService.clincid
          });


           this.only (medicalService.isActive==1);
          this.dialog.open(
            this.addMedicalServiceDialog,
            {
              width: '600px'
            }
          );

        },

        error: (error) => {

          console.error(
            'API ERROR:',
            error
          );

        }

      });
    this.getClinclist()
}
saveMedicalService(): void {

    if (this.isEditMode) {

      this.updateMedicalService();

    } else {

      this.addMedicalService();

    }

}
updateMedicalService(): void {

  this.medicalService
    .update(
      this.selectedMedicalServiceId,
      this.MedicalServiceData()
    )
    .subscribe({

      next: (response) => {


        if(response){
        this.loadMedicalServices();

        this.dialog.closeAll();

        this.dialog.open(
          this.successDialog,
          {
            width: '400px'
          }
        );}
        else{


           this.dialog.open(
          this.deleteErrorDialog);

        }

      },

     error: (error) => {

  console.error('API ERROR:', error);

  const errors = error.error?.errors;

  if (errors) {

    const messages: string[] = [];

    Object.keys(errors).forEach(key => {

      messages.push(
        ...errors[key]
      );

    });

    this.errorMessage = messages.join('\n');

  } else {

    this.errorMessage =
      'Something went wrong.';

  }

  this.dialog.closeAll();

  this.dialog.open(
    this.errorDialog,
    {
      width: '400px'
    }
  );

}

    });

}
deleteMedicalService(id: number): void {

    this.selectedMedicalServiceId = id;

    const dialogRef =
      this.dialog.open(
        this.deleteDialog
      );

    dialogRef.afterClosed()
      .subscribe(result => {

        if (result === true) {

          this.confirmDelete();

        }

      });

}
confirmDelete(): void {

  this.medicalService
    .delete(this.selectedMedicalServiceId)
    .subscribe({

      next: (response) => {

        if(response)
        {
          this.loadMedicalServices();

          this.dialog.open(this.deleteSuccessDialog);

        }
        else{

           this.dialog.open(
          this.deleteErrorDialog
        );
        }


      },
      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

        this.dialog.open(
          this.deleteErrorDialog
        );

      }

    });

}
allowOnlyNumbers(
  event: KeyboardEvent): void {

  const key = event.key;

  if (
    !/^[0-9]$/.test(key) &&
    key !== 'Backspace' &&
    key !== 'Delete' &&
    key !== 'ArrowLeft' &&
    key !== 'ArrowRight' &&
    key !== 'Tab'
  ) {

    event.preventDefault();

  }

}
only(isChecked: boolean): void {
  const nameControl = this.medicalServiceForm.get('serviceName');
  const desrControl = this.medicalServiceForm.get('desription');
  const priceControl = this.medicalServiceForm.get('price');
  const durationControl = this.medicalServiceForm.get('durationInMinutes');
  const costControl = this.medicalServiceForm.get('costPrice');
  const sellingControl = this.medicalServiceForm.get('sellingPrice');
  const clincid = this.medicalServiceForm.get('clincid');


  if (!isChecked&&this.isEditMode==true) {
    nameControl?.disable();
    desrControl?.disable();
    priceControl?.disable();
    durationControl?.disable();
    costControl?.disable();
    sellingControl?.disable();
    clincid?.disable();
  } else {
   nameControl?.enable();
    desrControl?.enable();
    priceControl?.enable();
    durationControl?.enable();
    costControl?.enable();
    sellingControl?.enable();
    clincid?.enable();
  }
}
MedicalServiceData() {

  return {
    serviceName: this.medicalServiceForm.get('serviceName')?.value,
    serviceCode: this.medicalServiceForm.get('serviceCode')?.value,
    desription: this.medicalServiceForm.get('desription')?.value,
    price: this.medicalServiceForm.get('price')?.value,
    durationInMinutes: this.medicalServiceForm.get('durationInMinutes')?.value,
    costPrice: this.medicalServiceForm.get('costPrice')?.value,
    sellingPrice: this.medicalServiceForm.get('sellingPrice')?.value,
    isActive: this.medicalServiceForm.get('isActive')?.value ? 1 : 0,
        clincid: this.medicalServiceForm.get('clincid')?.value
  };

}
getClinclist():void{

  this.medicalService.getClinclist()
       .subscribe({
        next: (data) => {

          this.Clincs = data;

          this.cdr.detectChanges();

        },
        error: (error) => {

          console.error(
            ' API ERROR:',
            error
          );

        }

      });






}

}
