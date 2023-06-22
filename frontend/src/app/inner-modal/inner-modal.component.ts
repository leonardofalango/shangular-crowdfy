import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-inner-modal',
  templateUrl: './inner-modal.component.html',
  styleUrls: ['./inner-modal.component.css'],
  standalone: true,
  imports: [MatDialogModule, MatButtonModule]
})
export class InnerModalComponent {

}
