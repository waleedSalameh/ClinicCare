import {ChangeDetectorRef,Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import {FormsModule,ReactiveFormsModule,FormBuilder,FormGroup,Validators} from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDialogModule,MatDialog} from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { specialtyModels } from '../../models/specialtyModels';
import { SpecialtyService } from './services/SpecialtyServices';
@Component({
  selector: 'app-specialty',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,MatTableModule,MatButtonModule,MatIconModule,MatFormFieldModule,MatInputModule,MatDialogModule,MatSelectModule,MatCheckboxModule],
  templateUrl: './specialty.html',
  styleUrl: './specialty.css'
})
export class SpecialtyComponent implements OnInit {
  specialties: specialtyModels[] = [];
  name: string = '';
  code: string = '';
 isActive: boolean | null = null;
  isEditMode: boolean = false;
  selectedSpecialtyId: number = 0;
  specialtyForm: FormGroup;
  errorMessage: string = '';
  displayedColumns: string[] = ['id','name','code','isActive','actions'];
  @ViewChild('specialtyDialog')
  specialtyDialog!: TemplateRef<any>;
  @ViewChild('deleteDialog')
  deleteDialog!: TemplateRef<any>;
  @ViewChild('successDialog')
  successDialog!: TemplateRef<any>;
  @ViewChild('errorDialog')
  errorDialog!: TemplateRef<any>;
  constructor(
    private specialtyService: SpecialtyService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder
  ) {

    this.specialtyForm = this.fb.group({

      name: ['',Validators.required],
      code: ['',Validators.required],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadSpecialties();
  }
  loadSpecialties(): void {

    this.specialtyService
      .getAll(this.name, this.code,this.isActive)
      .subscribe({

        next: (response) => {

          this.specialties = response;

          this.cdr.detectChanges();

        },

        error: (error) => {

          console.error('API ERROR:', error);

        }

      });

  }
  search(): void {

    this.loadSpecialties();

  }
  clearFilters(): void {

    this.name = '';
    this.code = '';
    this.isActive = null;

    this.loadSpecialties();

  }
  openAddSpecialty(): void {

    this.isEditMode = false;

    this.selectedSpecialtyId = 0;

    this.specialtyForm.reset({

      name: '',
      code: '',
      isActive: true

    });

    this.dialog.open(
      this.specialtyDialog,
      {
        width: '500px'
      }
    );

  }
  openEditSpecialty(id: number): void {

    this.isEditMode = true;

    this.specialtyService
      .getById(id)
      .subscribe({

        next: (specialty) => {

          this.selectedSpecialtyId = specialty.id;

          this.specialtyForm.patchValue({

            name: specialty.name,
            code: specialty.code,
            isActive: specialty.isActive

          });
          this.only (specialty.isActive);
          this.dialog.open(
            this.specialtyDialog,
            {
              width: '500px'
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

  }
  saveSpecialty(): void {

    if (this.specialtyForm.invalid) {

      this.specialtyForm.markAllAsTouched();

      return;

    }

    if (this.isEditMode) {

      this.updateSpecialty();

    } else {

      this.addSpecialty();

    }

  }
 addSpecialty(): void {

  this.specialtyService
    .create(this.specialtyForm.value)
    .subscribe({

      next: (response) => {

        this.loadSpecialties();

        this.dialog.closeAll();

        this.dialog.open(
          this.successDialog,
          {
            width: '400px'
          }
        );

      },

      error: (error) => {

        console.error('API ERROR:', error);

        this.errorMessage =
          error.error?.message ||
          'Something went wrong.';



        this.dialog.open(
          this.errorDialog,
          {
            width: '400px'
          }
        );

      }

    });

}
  updateSpecialty(): void {

    this.specialtyService
      .update(
        this.selectedSpecialtyId,
        this.SpecialtyData()
      )
      .subscribe({

        next: (response) => {

          this.loadSpecialties();

          this.dialog.closeAll();

          this.dialog.open(
            this.successDialog,
            {
              width: '400px'
            }
          );

        },

       error: (error) => {

  console.log(error.error);

  this.errorMessage =
    error.error?.message ||
    'Something went wrong.';



  this.dialog.open(
    this.errorDialog,
    {
      width: '400px'
    }
  );

}

      });

  }
  deleteSpecialty(id: number): void {

    this.selectedSpecialtyId = id;

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

    this.specialtyService
      .delete(this.selectedSpecialtyId)
      .subscribe({

        next: (response) => {

          this.loadSpecialties();

        },

        error: (error) => {

        console.error('API ERROR:', error);

        this.errorMessage =
          error.error?.message ||
          'Something went wrong.';



        this.dialog.open(
          this.errorDialog,
          {
            width: '400px'
          }
        );

      }

      });

  }
  only(isChecked: boolean): void {

  const nameControl = this.specialtyForm.get('name');

  const codeControl = this.specialtyForm.get('code');

  if (!isChecked && this.isEditMode == true) {

    nameControl?.disable();

    codeControl?.disable();

  } else {

    nameControl?.enable();

    codeControl?.enable();

  }
  }
  SpecialtyData() {

  return {

    name: this.specialtyForm.get('name')?.value,

    code: this.specialtyForm.get('code')?.value,

    isActive: this.specialtyForm.get('isActive')?.value

  };

  }

}
