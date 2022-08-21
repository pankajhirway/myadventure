import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { AdventureService } from '../adventure.service';
import { Adventure } from '../node.model';

@Component({
  selector: 'app-select.adventure',
  templateUrl: './select.adventure.component.html',
  styleUrls: ['./select.adventure.component.css']
})
export class SelectAdventureComponent implements OnInit {

  constructor(private adventureService: AdventureService) { }

  adventures: Adventure[] = [];

  selectedValue: string = "";

  IsSelected: boolean = false;

  ngOnInit(): void {
    this.adventureService.getAdventuresV1().subscribe( (data) => {
        this.adventures = data;
        console.log(this.adventures);
    });
 }

 onSelectionChanged(event: MatSelectChange) {
   console.log('on ng model change', event);
   this.selectedValue = event.value;
   if (this.selectedValue != "")
   {
    this.IsSelected = true;
   } else {
    this.IsSelected = false;
   }
 }
}
