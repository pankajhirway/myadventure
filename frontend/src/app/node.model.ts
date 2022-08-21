export interface MyNode {
    id: string;
    label: string;
    isSelected:boolean
  }

export interface Link {
    id: string;
    source: string;
    target: string;
    label: string
    isSelected:boolean
  }

    export interface Option {
        value: string;
        nextId: string;
    }

    export interface Step {
        id: string;
        question: string;
        options: Option[];
    }

    export interface Adventure {
        id: string;
        name: string;
        steps: Step[];
    }

    export interface StepsTaken {
      stepId: string;
      optionTaken: string;
  }

  export interface GameSession {
      id: string;
      adventureId: string;
      stepsTaken: StepsTaken[];
      isComplete: boolean;
  }


