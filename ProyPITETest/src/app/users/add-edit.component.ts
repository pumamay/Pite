import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { UserService, AlertService } from '../_services';
import { MustMatch } from '../_helpers';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
  form!: FormGroup;
  id?: string;
  title!: string;
  loading = false;
  submitting = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];

    this.form = this.formBuilder.group({
      fullName: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      birthDay: ['', Validators.required],
      // password and confirm password only required in add mode
      password: ['', [Validators.minLength(6), ...(!this.id ? [Validators.required] : [])]],
      confirmPassword: ['', [...(!this.id ? [Validators.required] : [])]]
    }, {
      validators: MustMatch('password', 'confirmPassword')
    });

    this.title = 'Agregar Usuario';
    if (this.id) {
      // edit mode
      this.title = 'Editar Usuario';
      this.loading = true;
      this.userService.getById(this.id)
        .pipe(first())
        .subscribe(x => {
          this.form.patchValue(x);
          this.loading = false;
        });
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.submitting = true;
    this.saveUser()
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Usuario guardado', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/users');
        },
        error: error => {
          this.alertService.error(error);
          this.submitting = false;
        }
      })
  }

  private saveUser() {
    // create or update user based on id param
    return this.id
      ? this.userService.update(this.id!, this.form.value)
      : this.userService.create(this.form.value);
  }
}
