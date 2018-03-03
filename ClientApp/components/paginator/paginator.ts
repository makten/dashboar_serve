import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
//Core components
import Utils from '../../core/Utils';

@Component
export default class PaginatorComponent extends Vue {


    @Prop({})
    totalItems: number;

    @Prop({type: Number, default: 10})
    pageSize: any;

    
    pages: any[] = [];
    currentPage = 1;


    get paginationRange () {

        this.currentPage = 1;

        let pagesCount = Math.ceil(this.totalItems / this.pageSize);
        this.pages = [];

        for (let i = 1; i <= pagesCount; i++)
            this.pages.push(i);        

        return this.pages;


    }

    pageChanged (page) {
        this.currentPage = page;
        this.$emit('page-changed', page);
    }

    previous () {
        if (this.currentPage == 1)
            return;

        this.currentPage--;
        this.$emit('page-changed', this.currentPage);       

    }

    next () {
        if (this.currentPage == this.pages.length)
            return;

        this.currentPage++;
        
        this.$emit('page-changed', this.currentPage);
    }


    // @Prop({ type: Number, default: 1, required: true })
    // currentPage: any;

    // @Prop({ type: Number, required: true })
    // totalPages: any;

    // @Prop({ type: Number, required: false })
    // itemsPerPage: any;

    // @Prop({ type: Number, required: false })
    // totalItems: any;

    // @Prop({ type: Number, default: 5 })
    // visiblePages: any;



    // // get computedVisiblePages() {
    // //     return this.visiblePages        
    // // }

    // get lastPage() {
    //     if (this.totalPages) {
    //         return this.totalPages;
    //     } else {
    //         return this.totalItems % this.itemsPerPage === 0
    //             ? this.totalItems / this.itemsPerPage
    //             : Math.floor(this.totalItems / this.itemsPerPage)
    //     }
    // }

    // get paginationRange() {
        
    //     let start = this.currentPage - this.visiblePages / 2 <= 0
    //         ? 1 : this.currentPage + this.visiblePages / 2 > this.lastPage
    //             ? Utils.lowerBound(this.lastPage - this.visiblePages + 1, 1)
    //             : Math.ceil(this.currentPage - this.visiblePages / 2);

    //     let range = [];

    //     for (let i = 0; i < this.visiblePages && i < this.lastPage; i++){
    //         range.push(start + i)
    //     }

    //     return range;
    // }


    // activePage (pageNum) {
        
    //     return this.currentPage === pageNum ? 'active' : '';
    // }

    // pageChanged (pageNum) {
    //     // this.currentPage = pageNum;        
    //     this.$emit("page-changed", pageNum);
        
    // }

    
}

