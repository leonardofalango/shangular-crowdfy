import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { Forum } from 'src/services/Forum';
import { ForumService } from 'src/services/ForumService';


/**
 * @title Multi-select autocomplete
 */
@Component({
  selector: 'app-test',
  templateUrl: 'test.component.html',
  styleUrls: ['test.component.css'],
})
export class TestComponent implements OnInit {

  constructor(private forumService: ForumService) { }

  forumControl = new FormControl();

  subscribedForums: Forum[] = [];

  selectedForums: Forum[] = new Array<Forum>();

  filteredForums: Observable<Forum[]> = new Observable();
  lastFilter: string = '';

  ngOnInit() {
    
    this.forumService
      .getAll()
      .subscribe(
        res =>
        this.subscribedForums = res
      )
      
    this.subscribedForums.forEach(
      elem => elem.selected = false
    )
  }

  filter(filter: string): Forum[] {
    this.lastFilter = filter;
    if (filter) {
      return this.subscribedForums.filter((option: { title: string; }) => {
        return option.title.toLowerCase().indexOf(filter.toLowerCase()) >= 0
      })
    } else {
      return this.subscribedForums.slice();
    }
  }

  displayFn(value: Forum[] | string): string {
    let displayValue: string = '';
    if (Array.isArray(value)) {
      value.forEach((forum, index) => {
        if (index === 0) {
          displayValue = forum.title
        } else {
          displayValue += ', ' + forum.title
        }
      });
    } else {
      displayValue = value;
    }
    return displayValue;
  }

  optionClicked(event: Event, forum: Forum) {
    event.stopPropagation();
    this.toggleSelection(forum);
  }

  toggleSelection(forum: Forum) {
    forum.selected = !forum.selected;
    if (forum.selected) {
      this.selectedForums.push(forum);
    } else {
      const i = this.selectedForums.findIndex(value => value.title === forum.title && value.description === forum.description);
      this.selectedForums.splice(i, 1);
    }

    this.forumControl.setValue(this.selectedForums);
  }

}