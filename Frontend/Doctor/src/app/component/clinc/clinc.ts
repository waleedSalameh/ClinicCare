import {ChangeDetectorRef,Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import { ClincModele } from './models/clinc.models';
import { ClincService } from './services/clinc.sevice';
import { FormsModule, Validators } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDialogModule,MatDialog} from '@angular/material/dialog';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { doctorModels } from '../../models/doctorModels';
@Component({
  imports: [FormsModule,ReactiveFormsModule,MatTableModule,MatButtonModule,CommonModule,MatIconModule,MatFormFieldModule,MatInputModule,MatDialogModule,MatSlideToggleModule,MatSelectModule,MatCheckboxModule],
  selector: 'app-clinc',
  styleUrl: './clinc.css',
  templateUrl: './clinc.html',
})
export class ClincComponent implements OnInit {
  doctorNos: doctorModels[] = [];
  ClincServices: ClincModele[] = [];
  isEditMode: boolean = false;
  isReadOnly:boolean= true;
  displayedColumns: string[] = ['id','clincName','clincCode','specialization','doctorNo','NumberOfPatients','time','code','isActive','actions'];
  selectedClincId: number = 0;
  ClincForm: FormGroup;
  errorMessage: string = '';
 isActive: boolean | null = null;
   clincName: string = '';
  clincCode:number | null = null;


   @ViewChild('addClincDialog')
  addClincDialog!: TemplateRef<any>;
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
     private ClincService: ClincService,
     private cdr: ChangeDetectorRef,
     private dialog: MatDialog,
     private fb: FormBuilder

  ) {
     this.ClincForm = this.fb.group({
      clincName: ['',Validators.required],
      clincCode: [{ value: '', disabled: true }],
      specialization: ['',Validators.required],
      doctorNo: [null,Validators.required],
      isActive: true,
      numberOfPatients:[0,Validators.required],
       startTime: ['',Validators.required],
      endTime: [ '', Validators.required],
      code:['',Validators.required]
    });
  }


ngOnInit(): void {
this.loadClincService();

}
loadClincService(): void {

    this.ClincService.getAll(this.clincName,this.clincCode,this.isActive,).subscribe({

      next: (response) => {

        this.ClincServices = response;

        this.cdr.detectChanges();

      },

      error: (error) => {

        console.error(
          'API ERROR:',
          error
        );

      }

    });
    this.getDocterlist()

}
openAddClinc():void{
  this.isEditMode = false;
  this.selectedClincId = 0;
     let nextCode = 1;

  if (this.ClincServices.length > 0) {

    const lastCode = Math.max(
      ...this.ClincServices.map(x => Number(x.clincCode))
    );
    nextCode = lastCode + 1;
  }
  this.ClincForm.reset({
    clincName:'',
    clincCode:nextCode,
    specialization:'',
    startTime: '',
    endTime: '',
    doctorNo: null,
    NumberOfPatients: 0,
    isActive: true
  });
   this.dialog.open(
      this.addClincDialog,
      {
        width: '600px'
      }
    );
this.getDocterlist()
}
addClinc(): void {

  this.ClincService
  .create(this.ClincData())
    .subscribe({

      next: (response) => {

         if(response){
        this.loadClincService();
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

}

    });

}
openEditClinc(id: number): void {

  this.isEditMode = true;

  this.ClincService.getById(id).subscribe({

    next: (clinc) => {

      this.selectedClincId = clinc.id;

      this.ClincForm.patchValue({

        clincName: clinc.clincName,
        clincCode: clinc.clincCode,
        specialization: clinc.specialization,
        doctorNo: clinc.doctorNo,
        numberOfPatients: clinc.numberOfPatients,
        code:clinc.code,
         startTime:
              clinc.startTime
                ?.toString()
                .substring(0, 5),

            endTime:
              clinc.endTime
                ?.toString()
                .substring(0, 5),
        isActive: clinc.isActive

      });
         this.only (clinc.isActive);
      this.dialog.open(
        this.addClincDialog,
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
this.getDocterlist();
}
updateClinc(): void {


  this.ClincService
    .update(
      this.selectedClincId,
      this.ClincData()
    )
    .subscribe({

      next: (response) => {

        if (response) {

          this.loadClincService();

          this.dialog.closeAll();

          this.dialog.open(
            this.successDialog,
            {
              width: '400px'
            }
          );

        } else {

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


          const errors =
            error.error?.errors;


          if (errors) {

            const messages: string[] = [];


            Object.keys(errors)
              .forEach(key => {

                messages.push(
                  ...errors[key]
                );

              });


            this.errorMessage =
              messages.join('\n');

          } else {

            this.errorMessage =
              error.error?.message ||
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
deleteClinc(id: number): void {

  this.selectedClincId = id;

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

  this.ClincService
    .delete(this.selectedClincId)
    .subscribe({

      next: (response) => {

        if (response) {

          this.loadClincService();

          this.dialog.open(
            this.deleteSuccessDialog
          );

        } else {

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
save(): void {

    if (this.isEditMode) {

      this.updateClinc();

    } else {

      this.addClinc();

    }

}
// only(isActive: boolean):void{
//  if(isActive==false&&this.isEditMode==true){
// this.isReadOnly=true;
// }
// if(isActive==true&&this.isEditMode==true){
// this.isReadOnly=false;
// }
only(isChecked: boolean): void {
  const nameControl = this.ClincForm.get('clincName');
  const specControl = this.ClincForm.get('specialization');
  const codeControl = this.ClincForm.get('code');
  const doctorControl = this.ClincForm.get('doctorNo');
  const patientsControl = this.ClincForm.get('numberOfPatients');
  const startTimeControl = this.ClincForm.get('startTime');
  const endTimeControl = this.ClincForm.get('endTime');



  if (!isChecked&&this.isEditMode==true) {
    nameControl?.disable();
    specControl?.disable();
    codeControl?.disable();
    doctorControl?.disable();
    patientsControl?.disable();
    startTimeControl?.disable();
    endTimeControl?.disable();
  } else {
    nameControl?.enable();
    specControl?.enable();
    codeControl?.enable();
    doctorControl?.enable();
    patientsControl?.enable();
    startTimeControl?.enable();
    endTimeControl?.enable();
  }
}

ClincData() {

  return {
    clincName: this.ClincForm.get('clincName')?.value,
    clincCode: this.ClincForm.get('clincCode')?.value,
    specialization: this.ClincForm.get('specialization')?.value,
    code: this.ClincForm.get('code')?.value,
    doctorNo: this.ClincForm.get('doctorNo')?.value,
    numberOfPatients: this.ClincForm.get('numberOfPatients')?.value,
    startTime: this.ClincForm.get('startTime')?.value+ ':00',
    endTime: this.ClincForm.get('endTime')?.value+ ':00',
    isActive: this.ClincForm.get('isActive')?.value
  };

}
clearFilters(): void {

    this.clincName = '';

    this.clincCode = null;
    this.isActive = null;

    this.loadClincService();

}
search(): void {

    this.ClincService
      .getAll(
        this.clincName,
         this.clincCode,
         this.isActive
      )
      .subscribe({

        next: (response) => {

          this.ClincServices = response;

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
getDocterlist():void{

  this.ClincService.getDocterlist()
       .subscribe({
        next: (data) => {

          this.doctorNos = data;

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



