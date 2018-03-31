<template>
  <div class="m2 flex flex-column items-center">
    <div class="table bg-white shadow2 m2">
      <div class="table-row divider-bottom">
        <div
          v-for="column in columns"
          :key="column.name"
          :class="{ bold: sortColumn === column.name }"
          class="table-cell px4 py2 no-select pointer"
          @click="sortBy(column)">
          {{ column.title }}
          <span v-if="sortColumn === column.name && reverse">↑</span>
          <span v-if="sortColumn === column.name && !reverse">↓</span>
        </div>
      </div>
      <div
        v-for="(row, index) in rows"
        :key="index"
        class="table-row divider-bottom">
        <div
          v-for="column in columns"
          :key="column.name"
          class="table-cell px4 py2">
          {{ row[column.name] }}
        </div>
      </div>
    </div>
    <Pager
      v-model="currentPage"
      :records="value.length"
      :per-page="perPage" />
  </div>
</template>

<script>
  import { sortBy } from "lodash";

  export default {
    name: "List",
    props: {
      value: {
        type: Array,
        required: true,
      },
      columns: {
        type: Array,
        required: true,
      },
      perPage: {
        type: Number,
        required: false,
        default: 5,
      },
    },
    data() {
      return {
        currentPage: 1,
        sortColumn: null,
        reverse: false,
      };
    },
    computed: {
      rows() {
        const sorted = sortBy(this.value, [this.sortColumn]);
        const reversed = this.reverse ? sorted.reverse() : sorted;
        const offset = (this.currentPage - 1) * this.perPage;
        return reversed.slice(offset, this.perPage + offset);
      },
    },
    methods: {
      sortBy({ name }) {
        this.reverse = this.sortColumn === name ? !this.reverse : false;
        this.sortColumn = name;
      },
    },
  };
</script>
