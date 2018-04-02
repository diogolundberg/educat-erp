<template>
  <span class="flex gutters flex-wrap">
    <template v-for="item in options">
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
