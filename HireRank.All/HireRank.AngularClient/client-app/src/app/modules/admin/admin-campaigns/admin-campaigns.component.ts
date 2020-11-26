import { Component, OnInit } from '@angular/core';
import { NzTableLayout, NzTablePaginationPosition, NzTableQueryParams, NzTableSize } from 'ng-zorro-antd/table';
import { PagedResult } from "../../../shared/models/paged-result.model";
import { Campaign } from "../../../shared/models/campaign.model";
import { GetAdminCampaign } from "../../../shared/models/get-admin-campaigns.model";
import { CampaignService } from "../../../core/services/campaign.service";
import { NzMessageService } from 'ng-zorro-antd/message';

interface Setting {
  bordered: boolean;
  loading: boolean;
  pagination: boolean;
  sizeChanger: boolean;
  title: boolean;
  header: boolean;
  footer: boolean;
  expandable: boolean;
  checkbox: boolean;
  fixHeader: boolean;
  noResult: boolean;
  ellipsis: boolean;
  simple: boolean;
  size: NzTableSize;
  tableScroll: string;
  tableLayout: NzTableLayout;
  position: NzTablePaginationPosition;
}

@Component({
  selector: 'app-admin-campaigns',
  templateUrl: './admin-campaigns.component.html',
  styleUrls: ['./admin-campaigns.component.css']
})
export class AdminCampaignsComponent implements OnInit {
  
  campaigns: PagedResult<Campaign> = {
    totalCount: 1,
    items: new Array<Campaign>()
  };
  loading: Boolean = false;
  queryFilter: GetAdminCampaign = new GetAdminCampaign();
  totalСount: 1;
  nameFilter: string = " ";
  startDateFilter: Date[];
  endDateFilter: Date[];
  nameFilterVisible: boolean = false;
  startDateFilterVisible: false;
  endDateFilterVisible: false;
  isCreatingFormVisible:boolean = false;

  constructor(private campaignService: CampaignService, private messageService: NzMessageService) { }

  ngOnInit(): void {
    this.queryFilter.pageSize = 2;
    this.queryFilter.pageNumber = 1;
    this.loadCampaigns();
  }

  loadCampaigns(): void {
    this.loading = true;
    this.campaignService
      .getAdminCampaigns(this.queryFilter)
      .subscribe(result => {
        this.campaigns =  result;
        this.loading = false;
      });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, sort, filter } = params;
    const currentSort = sort.find(item => item.value !== null);
    const sortField = (currentSort && currentSort.key) || null;
    const sortOrder = (currentSort && currentSort.value) || null;

    this.queryFilter.pageNumber =  pageIndex;
    this.queryFilter.pageSize = pageSize;
    this.queryFilter.sortingProperty = sortField;
    this.queryFilter.sortingOrder = ( sortOrder == "ascend") ? "asc" : "desc";
    this.loadCampaigns();
  }

  onChangeStartDateFilter(result: Date[]) {
    this.queryFilter.startDateFrom = result[0];
    this.queryFilter.startDateTo = result[result.length - 1];
    
  }

  onChangeEndDateFilter(result: Date[]) {
    this.queryFilter.endDateFrom = result[0];
    this.queryFilter.endDateTo = result[result.length - 1];
    
  }

  searchByName() {
    this.queryFilter.name = this.nameFilter;
    this.nameFilterVisible = false;

    this.loadCampaigns();
  }

  resetFilterByName() {
    this.nameFilter = '';
    this.queryFilter.name = this.nameFilter;
    this.nameFilterVisible = false;

    this.loadCampaigns();
  }

  searchByStartDate() {
    this.startDateFilterVisible = false;
    this.loadCampaigns();
  }

  resetFilterByStartDate() {
    this.startDateFilter = [];
    this.queryFilter.startDateFrom = null;
    this.queryFilter.startDateTo = null;
    this.startDateFilterVisible = false;

    this.loadCampaigns();
  }

  searchByEndDate() {
    this.endDateFilterVisible = false;
    this.loadCampaigns();
  }

  resetFilterByEndDate() {
    this.endDateFilter = [];
    this.queryFilter.endDateFrom = null;
    this.queryFilter.endDateTo = null;
    this.endDateFilterVisible = false;

    this.loadCampaigns();
  }

  afterCampaignCreating(value: boolean) {
    if (value) {
      this.messageService.success("Ви успішно створили ярмарку вакансій!");
    }
    else {
      this.messageService.error("Трапилась помилка при створенні ярмарки вакансій!");
    }
    this.hideCreatingForm();
    this.loadCampaigns();
  }

  hideCreatingForm(){
    this.isCreatingFormVisible = !this.isCreatingFormVisible
  }
}
