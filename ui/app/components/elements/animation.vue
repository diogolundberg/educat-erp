<template>
  <span>
    <template v-if="name == 'success'">
      <div class="anim-icon anim-success animate">
        <span class="anim-line anim-tip" />
        <span class="anim-line anim-long" />
        <div class="anim-placeholder" />
        <div class="anim-fix" />
      </div>
    </template>
    <template v-if="name == 'error'">
      <div class="anim-icon anim-error animate">
        <span class="anim-x-mark">
          <span class="anim-line anim-left animateXLeft" />
          <span class="anim-line anim-right animateXRight" />
        </span>
        <div class="anim-placeholder" />
        <div class="anim-fix" />
      </div>
    </template>
    <template v-if="name == 'warning'">
      <div class="anim-icon anim-warning">
        <span class="anim-body pulseWarningIns" />
        <span class="anim-dot pulseWarningIns" />
      </div>
    </template>
  </span>
</template>

<script>
  export default {
    name: "Animation",
    props: {
      name: {
        type: String,
        default: "success",
      },
    },
  };
</script>
