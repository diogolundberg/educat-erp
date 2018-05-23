<template>
  <span class="flex gutters flex-wrap">
    <template v-for="item in filteredOptions">
      <Checkbox
        :value="isChecked(item)"
        :label="item[labelField]"
        :key="item.id"
        :class="{ 'sm-col-3': !vertical }"
        class="col-12 py1 "
        @input="!disabled && toggle(item)" />
    </template>
  </span>
</template>

<script>
  import { xor } from "lodash";

  export default {
    name: "CheckGroup",
    props: {
      value: {
        type: Array,
        required: true,
      },
      options: {
        type: Array,
        required: true,
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      vertical: {
        type: Boolean,
        default: false,
      },
      filterKey: {
        type: String,
        default: null,
      },
      filter: {
        type: [String, Number, Array],
        default: null,
      },
    },
    computed: {
      filteredOptions() {
        return this.options.filter(a => !this.filterKey || a[this.filterKey] ===
          this.filter || this.filter.includes(a[this.filterKey]));
      },
    },
    watch: {
      filter() {
        const keys = this.filteredOptions.map(a => a[this.idField]);
        const values = this.value.filter(a => keys.includes(a));
        this.$emit("input", values);
      },
    },
    methods: {
      isChecked(option) {
        return !!this.value.includes(option[this.idField]);
      },
      toggle(option) {
        this.$emit("input", xor(this.value, [option[this.idField]]));
      },
    },
  };
</script>
