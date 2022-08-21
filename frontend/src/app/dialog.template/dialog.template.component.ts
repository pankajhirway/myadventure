import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute,ParamMap } from '@angular/router';
import { DagreNodesOnlyLayout, Layout } from '@swimlane/ngx-graph';
import { switchMap } from 'rxjs';
import { AdventureService } from '../adventure.service';
import { Adventure, GameSession, Link, MyNode, Step } from '../node.model';

export interface DialogData {
  question: string;
  step: Step;
  selectedOption: SelectionOption;
  IsFinished: boolean;
}

export interface SelectionOption {
  value: string;
  next: string;
  stepId: string;
}

@Component({
  selector: 'app-dialog.template',
  templateUrl: './dialog.template.component.html',
  styleUrls: ['./dialog.template.component.css']
})

export class DialogTemplateComponent implements OnInit {

  constructor(public dialog: MatDialog,private route: ActivatedRoute, private adventureService: AdventureService) { }

  animal: string | undefined;
  name: string | undefined;
  id: string = "";
  session: GameSession | any;
  currentAdventure: Adventure | any = {};
  public nodes : MyNode[] = []

  public links : Link[] = []

  public layoutSettings = {
    orientation: 'TB'
  };
  public layout: Layout = new DagreNodesOnlyLayout();
  public dialogRef: MatDialogRef<DialogOverviewExampleDialog,any>;
  ngOnInit(): void {
    this.session = {};
     this.id = this.route.snapshot.paramMap.get('id')!;
     this.adventureService.startAdventuresV1(this.id).subscribe(resp => {
    this.session = resp;
    this.adventureService.Adventures.forEach(a => {
        if(a.id == this.id)
        {
          this.currentAdventure = a;
          this.startAdventure();
        }
      })
      console.log(resp);
     })
  }

  startAdventure() {
   var start =  this.currentAdventure?.steps[0];
   if(start)
   {
    this.openDialog(start);
   }
  }

  nextQuestion(previous: SelectionOption) {
    if (previous) {
      var start =  this.currentAdventure.steps.find((s: { id: string; }) => s.id == previous.next)
      if(start)
      {
       this.openDialog(start);
      }
    }

  }

  
  openDialog(step: Step): void {
     this.dialogRef = this.dialog.open(DialogOverviewExampleDialog, {
      width: '450px',
      data: {question: step.question, step : step},
      disableClose: true
    });

    this.dialogRef.afterClosed().subscribe((result: string) => {
      const data = this.dialogRef.componentInstance.data;
      this.adventureService.recordStep(this.session.id,data.selectedOption.stepId,data.selectedOption.value).subscribe(adventuresSession =>{
        this.session = adventuresSession;
        console.log(this.session);
        this.PerformNext(data);
      });
    });
  }

  PerformNext(data: DialogData): void
  {
    if(!data.IsFinished)
      {
        this.nextQuestion(data.selectedOption); 
      }
      else {
        this.updateNodesAndLinks(this.currentAdventure,this.session);
      }
  }

  updateNodesAndLinks(adventure:Adventure,session: GameSession): void {
    this.nodes = [];
    this.links = [];
    adventure.steps.forEach(s =>{
          var taken = session.stepsTaken.find(st => st.stepId == s.id)
          var node = {} as MyNode;
          node.id = s.id;
          node.label = s.question;
          node.isSelected = taken ? true : false
          this.nodes.push(node);
          s.options.forEach(o => {
            var link = {} as Link;
            link.label = o.value
            link.target = o.nextId
            link.source = s.id
            link.isSelected = taken?.optionTaken == o.value ? true : false;
            this.links.push(link)
          })
       })
  }

}

@Component({
  selector: 'dialog-overview-example-dialog',
  templateUrl: 'htmldialog.html',
})
export class DialogOverviewExampleDialog {

  constructor(
    public dialogRef: MatDialogRef<DialogOverviewExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onClick(selectedOption: string, next: string,stepid: string): void {
    this.dialogRef.componentInstance.data.selectedOption = {value: selectedOption, next: next, stepId: stepid};
    if(next == '')
    {
      this.dialogRef.componentInstance.data.IsFinished = true;
    }
    this.dialogRef.close();
  }

  onClickEnd() {
    this.dialogRef.componentInstance.data.IsFinished = true;
    this.dialogRef.close();
  }

}
