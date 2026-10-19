import {Component,OnInit,TemplateRef,ViewChild} from '@angular/core';
import {ActivatedRoute,isActive,Router} from '@angular/router';
import {FormBuilder,FormControl,ReactiveFormsModule,Validators} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import {MatDialog,MatDialogModule} from '@angular/material/dialog';
import { DoctorService } from '../../services/doctor';
import { specialtyModels } from '../../models/specialtyModels';
@Component({
selector: 'app-doctor-form',
standalone: true,
imports: [ReactiveFormsModule,MatFormFieldModule,MatInputModule,MatCheckboxModule,MatButtonModule,MatSelectModule,MatDialogModule],
templateUrl: './doctor-form.html',
  styleUrl: './doctor-form.css'
})
export class DoctorFormComponent implements OnInit {
  doctorForm;
  // =========================
  // Specialties
  // =========================

  specialties: specialtyModels[] = [];

  isReadOnly:boolean= true;

  // =========================
  // Doctor Id
  // =========================

  // إذا كان عندنا id معناها Edit

  doctorId: number | null = null;


  // =========================
  // Success Popup
  // =========================

  @ViewChild('successDialog')

  successDialog!: TemplateRef<any>;


  // =========================
  // Error Popup
  // =========================

  @ViewChild('errorDialog')

  errorDialog!: TemplateRef<any>;


  constructor(
    private fb: FormBuilder,
    private doctorService: DoctorService,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {


    // =========================
    // Form
    // =========================

    this.doctorForm = this.fb.group({

      fullName: [''],
      specialtyId:
        new FormControl<number | null>(null,Validators.required),
      phoneNumber: [''],
      yearsOfExperience: [0],
      email: [''],
      gender: [''],
      isActive: [true],
      createdDateTime: ['']
    });
  }


  // =========================
  // On Init
  // =========================

  ngOnInit(): void {


    // =========================
    // Get Specialties
    // =========================

    this.getSpecialties();


    // =========================
    // Get Doctor Id
    // =========================

    const id =
      this.route.snapshot.paramMap.get('id');


    // =========================
    // Edit
    // =========================

    if (id) {

      this.doctorId =
        Number(id);


      console.log(
        'Edit Doctor ID:',
        this.doctorId
      );


      this.loadDoctor(
        this.doctorId
      );

    }

  }


  // =========================
  // Get Specialties
  // =========================

  getSpecialties(): void {


    this.doctorService

      .getSpecialties()

      .subscribe({

        next: (data) => {


          console.log(
            'Specialties:',
            data
          );


          this.specialties =
            data;

        },


        error: (error) => {


          console.error(
            'Get specialties error:',
            error
          );

        }

      });

  }


  // =========================
  // Load Doctor
  // =========================

  loadDoctor(
    id: number
  ): void {


    this.doctorService

      .getById(id)

      .subscribe({

        next: (doctor) => {


          console.log(
            'Doctor loaded:',
            doctor
          );


          this.doctorForm.patchValue({


            // Doctor Name

            fullName:
              doctor.fullName,


            // Specialty

            specialtyId:
              doctor.specialtyId,


            // Phone

            phoneNumber:
              doctor.phoneNumber,


            // Experience

            yearsOfExperience:
              doctor.yearsOfExperience,


            // Email

            email:
              doctor.email,


            // Gender

            gender:
              doctor.gender,


            // Status

            isActive:
              doctor.isActive,

            createdDateTime:
             doctor.createdDateTime

          });
          this.only(doctor.isActive);

        },


        error: (error) => {


          console.error(
            'Get doctor error:',
            error
          );

        }

      });

  }


  // =========================
  // Submit
  // =========================
  onSubmit(): void {


    // =========================
    // Specialty Validation Only
    // =========================

    if (
      this.doctorForm
        .get('specialtyId')
        ?.invalid
    ) {


      this.doctorForm
        .get('specialtyId')
        ?.markAsTouched();


      return;

    }


    // =========================
    // Form Data
    // =========================

    const doctor =
      this.DoctorData();


    // =========================
    // EDIT
    // =========================

    if (
      this.doctorId !== null
    ) {


      console.log(
        'Updating Doctor:',
        doctor
      );


      this.doctorService

        .update(
          this.doctorId,
          doctor
        )

        .subscribe({

          // =====================
          // Update Success
          // =====================

          next: (response: boolean) => {


            console.log(
              'Update Response:',
              response
            );


            if (
              response === true
            ) {


              console.log(
                'Doctor updated successfully'
              );


              const dialogRef =
                this.dialog.open(
                  this.successDialog,
                  {
                    width: '400px',
                    disableClose: true
                  }
                );


              dialogRef
                .afterClosed()
                .subscribe(() => {


                  this.router.navigate([
                    '/doctors'
                  ]);

                });

            }


            // =====================
            // Update Failed
            // =====================

            else {


              console.log(
                'Doctor update failed'
              );


              this.dialog.open(
                this.errorDialog,
                {
                  width: '400px',
                  disableClose: true
                }
              );

            }

          },


          // =====================
          // API Error
          // =====================

          error: (error) => {


            console.error(
              'Update doctor error:',
              error
            );


            this.dialog.open(
              this.errorDialog,
              {
                width: '400px',
                disableClose: true
              }
            );

          }

        });


      return;

    }


    // =========================
    // CREATE
    // =========================

    console.log(
      'Creating Doctor:',
      doctor
    );


    this.doctorService.create(doctor)

      .subscribe({

        // =====================
        // Create Success
        // =====================

        next: (response) => {


          console.log(
            'Doctor created successfully:',
            response
          );


          this.router.navigate([
            '/doctors'
          ]);

        },


        // =====================
        // Create Error
        // =====================

        error: (error) => {


          console.error(
            'Create doctor error:',
            error
          );

        }

      });

  }
  // =========================
  // Cancel
  // =========================
  cancel(): void {


    console.log(
      'Cancel button clicked'
    );


    this.router.navigate([
      '/doctors'
    ]);

  }
only(isChecked: boolean): void {
  const nameControl = this.doctorForm.get('fullName');
  const specControl = this.doctorForm.get('specialtyId');
  const phoneControl = this.doctorForm.get('phoneNumber');
  const yearsControl = this.doctorForm.get('yearsOfExperience');
  const emailControl = this.doctorForm.get('email');
  const genderControl = this.doctorForm.get('gender');
  const createdControl = this.doctorForm.get('createdDateTime');

  if (!isChecked) {
    nameControl?.disable();
    specControl?.disable();
    phoneControl?.disable();
    yearsControl?.disable();
    emailControl?.disable();
    createdControl?.disable();
    genderControl?.disable();

  } else {
    nameControl?.enable();
    specControl?.enable();
    phoneControl?.enable();
    yearsControl?.enable();
    emailControl?.enable();
    createdControl?.enable();
    genderControl?.enable();


  }
}
DoctorData() {

  return {

    fullName:
      this.doctorForm.get('fullName')?.value,

    specialtyId:
      this.doctorForm.get('specialtyId')?.value,

    phoneNumber:
      this.doctorForm.get('phoneNumber')?.value,

    yearsOfExperience:
      this.doctorForm.get('yearsOfExperience')?.value,

    email:
      this.doctorForm.get('email')?.value,

    gender:
      this.doctorForm.get('gender')?.value,

    isActive:
      this.doctorForm.get('isActive')?.value,

    createdDateTime:
      this.doctorForm.get('createdDateTime')?.value

  };

}

}
