import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector:'app-controls',
  standalone:true,
  templateUrl:'./controls.html'
})
export class ControlsComponent {

  @Output() undoClicked =
    new EventEmitter<void>();

  @Output() resetClicked =
    new EventEmitter<void>();

  @Output() scoreboardResetClicked =
    new EventEmitter<void>();
}