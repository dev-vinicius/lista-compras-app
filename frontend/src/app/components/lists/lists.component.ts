import { Component, OnInit } from '@angular/core';
import { List } from 'src/app/models/list.model';
import { ListService } from 'src/app/services/list.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  lists: List[] = []

  constructor(private listService: ListService) { }

  ngOnInit(): void {
    this.listService.getAllLists().subscribe(lists => {
      this.lists = lists;
      console.log(lists);
    })
  }

}
