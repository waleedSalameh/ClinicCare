import { ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { patientModels } from './models/patientmodels';
import { PatientService } from './services/patientsevices';
import {FormBuilder,FormGroup,ReactiveFormsModule,Validators} from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [
    FormsModule,
    DatePipe,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatSelectModule
  ],
  templateUrl: './patient.html',
  styleUrl: './patient.css'
})
export class PatientComponent implements OnInit {

  patients: patientModels[] = [];

  fullName: string = '';
  phoneNumber: string = '';
  nationalId: string = '';

  displayedColumns: string[] = [
    'id',
    'code',
    'fullName',
    'nationalId',
    'phoneNumber',
    'dateOfBirth',
    'gender',
    'address',
      'age',
    'actions'
  ];
  isEditMode: boolean = false;
  selectedPatientId: number = 0;
  patientForm: FormGroup;
  errorMessage: string = '';
 @ViewChild('addPatientDialog')addPatientDialog!: TemplateRef<any>;
  @ViewChild('successDialog')successDialog!: TemplateRef<any>;
  @ViewChild('errorDialog')errorDialog!: TemplateRef<any>;
  @ViewChild('deleteDialog') deleteDialog!: TemplateRef<any>;
  @ViewChild('deleteSuccessDialog') deleteSuccessDialog!: TemplateRef<any>;
  @ViewChild('deleteErrorDialog') deleteErrorDialog!: TemplateRef<any>;

  constructor(
    private patientService: PatientService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder

  ) {
   this.patientForm = this.fb.group({

      code: [{ value: 0, disabled: true }],
      fullName: ['',],
      nationalId: [''],
      phoneNumber: [''],
      dateOfBirth: [''],
      gender: [''],
      address: [''],
      age: [{ value: '', disabled: true }],
    });

  }
ngOnInit(): void {
    this.loadPatients();
    this.getAllData();
}
loadPatients(): void {

    this.patientService.getAll(
      this.fullName,
      this.phoneNumber,
      this.nationalId
    ).subscribe({

      next: (response) => {
        this.patients = response;
      },

      error: (error) => {
        console.log(error);
      }

    });
}
search(): void {
  this.patientService
    .getAll(this.fullName, this.phoneNumber, this.nationalId)
    .subscribe(response => {
      this.patients = response;
      this.cdr.detectChanges();
    });
}
clearFilters(): void {

    this.fullName = '';
    this.phoneNumber = '';
    this.nationalId = '';

    this.getAllData();
}
deletePatient(
    id: number): void {

    this.selectedPatientId = id;

    const dialogRef = this.dialog.open(this.deleteDialog);

    dialogRef.afterClosed().subscribe(result => {

      if (result === true) {
        this.confirmDelete();
        this.getAllData();
      }

    });
}
confirmDelete(): void {

    this.patientService.delete(this.selectedPatientId).subscribe({

      next: () => {

        this.loadPatients();

        this.dialog.open(this.deleteSuccessDialog);
        this.getAllData();

      },

      error: (error) => {

        console.log(error);

        this.dialog.open(this.deleteErrorDialog);

      }

    });
}
getAllData(): void {

    this.patientService.getAll()
      .subscribe({
        next: (data) => {

          this.patients = data;

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
openAddPatient(): void {

  this.isEditMode = false;

  this.patientForm.reset();

  const nextCode =
    this.patients.length > 0
      ? Math.max(...this.patients.map(x => x.code)) + 1
      : 1;

  this.patientForm.patchValue({
    code: nextCode
  });

  this.dialog.open(
    this.addPatientDialog,
    {
       width: '600px',

    }
  );
}
addPatient(): void {

  this.patientService.create(
    this.patientForm.value
  ).subscribe({

    next: (response) => {

      this.loadPatients();

      this.dialog.closeAll();

      this.dialog.open(
        this.successDialog,
        {
          width: '400px'
        }
      );
     this.getAllData();
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
        error.error?.message || 'Something went wrong.';

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
openEditPatient(id: number): void {

  this.isEditMode = true;

  this.patientService.getById(id).subscribe({

    next: (patient) => {

      this.selectedPatientId = patient.id;

      this.patientForm.patchValue({
        code: patient.code,
        fullName: patient.fullName,
        nationalId: patient.nationalId,
        phoneNumber: patient.phoneNumber,
         dateOfBirth: patient.dateOfBirth
          ? patient.dateOfBirth.substring(0, 10)
          : '',
        gender: patient.gender,
        address: patient.address,
        age: patient.age,
      });

      this.dialog.open(
        this.addPatientDialog,
        {
          width: '600px'
        }
      );

    },

    error: (error) => {

      console.error('API ERROR:', error);

    }

  });

}
savePatient(): void {

  if (this.patientForm.invalid) {

    this.patientForm.markAllAsTouched();

    return;
  }

  if (this.isEditMode) {

    this.updatePatient();

  } else {

    this.addPatient();

  }

}
updatePatient(): void {

  this.patientService.update(
    this.selectedPatientId,
    this.patientForm.value
  ).subscribe({

    next: (response) => {

      console.log(
        'Patient updated successfully:',
        response
      );

      this.loadPatients();

      this.dialog.closeAll();

      this.dialog.open(

    this.successDialog,
    {
      width: '400px'
    }

  )
  this.getAllData();

},

   error: (error) => {

      this.errorMessage =
        error.error?.message || 'Something went wrong.';

      this.dialog.open(this.errorDialog, {
        width: '400px'
      });

    }

  });

}
calculateAge(): void {

  const dateOfBirth = this.patientForm.get('dateOfBirth')?.value;

  if (!dateOfBirth) {
    this.patientForm.get('age')?.setValue('');
    return;
  }

  const birthDate = new Date(dateOfBirth);
  const today = new Date();

  // أقل من شهر
  const oneMonthLater = new Date(birthDate);
  oneMonthLater.setMonth(oneMonthLater.getMonth() + 1);

  if (oneMonthLater > today) {

    const days = Math.floor(
      (today.getTime() - birthDate.getTime()) /
      (1000 * 60 * 60 * 24)
    );

    this.patientForm.get('age')?.setValue(`${days} days`);

    return;
  }

  // أقل من سنة
  const oneYearLater = new Date(birthDate);
  oneYearLater.setFullYear(oneYearLater.getFullYear() + 1);

  if (oneYearLater > today) {

    let months =
      (today.getFullYear() - birthDate.getFullYear()) * 12 +
      today.getMonth() - birthDate.getMonth();
    this.patientForm.get('age')?.setValue(`${months} months`);

    return;
  }

  // سنة أو أكثر
  let years =
    today.getFullYear() - birthDate.getFullYear();


  this.patientForm.get('age')?.setValue(`${years} years`);
}
}


